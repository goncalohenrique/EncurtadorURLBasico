using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Dicionário em memória para guardar as URLs (optei por não usar DB nesse projeto)
var urlDatabase = new Dictionary<string, string>();

app.UseDefaultFiles();
app.UseStaticFiles();

// Rota da API que recebe o formulario
app.MapPost("/api/encurtar", ([FromBody] DadosUsuario dados, HttpContext context) =>
{
    // Código curto aleatório de 6 caracteres
    string codigoCurto = Guid.NewGuid().ToString()[..6];
    
    // Monta a URL encurtada com base no servidor atual
    string urlEncurtada = $"{context.Request.Scheme}://{context.Request.Host}/{codigoCurto}";

    urlDatabase[codigoCurto] = dados.UrlOriginal;

    var conteudoArquivo = $@"--- INFORMAÇÕES DO ENCURTADOR ---
    Nome: {dados.Nome}
    Idade: {dados.Idade}
    URL Original: {dados.UrlOriginal}
    URL Encurtada: {urlEncurtada}
    ---------------------------------";

    // Retorna a URL e o conteúdo do texto em formato JSON
    return Results.Ok(new {
        UrlEncurtada = urlEncurtada,
        ConteudoTexto = conteudoArquivo
    });
});

//  Pega o código da URL e redireciona para a original
app.MapGet("/{codigo}", (string codigo) =>
{
    if (urlDatabase.TryGetValue(codigo, out var urlOriginal))
    {
        return Results.Redirect(urlOriginal);
    }
    return Results.NotFound("URL não encontrada ou expirada.");
});

app.Run();

class DadosUsuario
{
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
    public string UrlOriginal { get; set; } = string.Empty;
}