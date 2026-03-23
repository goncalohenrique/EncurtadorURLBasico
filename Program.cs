using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Usei dictionary para linkar uma url a outra, e salvei em memoria(optei por nao usar DB)
var urlDatabase = new Dictionary<string, string>();

app.UseDefaultFiles();
app.UseStaticFiles();

// Rota da API que recebe o formulário atualizada
app.MapPost("/api/encurtar", ([FromBody] DadosUsuarioRequest dados, HttpContext context) =>
{
    string codigoFinal;

    // 1. Lógica para definir o código curto
    if (!string.IsNullOrWhiteSpace(dados.CodigoSugerido))
    {
        // Usuário escolheu um link personalizado
        string codigoLimpo = dados.CodigoSugerido.Trim().ToLower();

        // Validação extra no back-end
        if (!System.Text.RegularExpressions.Regex.IsMatch(codigoLimpo, "^[a-z0-9\\-_]+$"))
        {
             return Results.BadRequest(new { Mensagem = "Código personalizado inválido. Use apenas letras, números, - e _." });
        }

        // Verificando se a url encurtada ja existe
        if (urlDatabase.ContainsKey(codigoLimpo))
        {
            return Results.Conflict(new { Mensagem = $"O link personalizado '{codigoLimpo}' já está em uso. Escolha outro." });
        }

        codigoFinal = codigoLimpo;
    }
    else
    {
        //  Usuário quer um link aleatório
        codigoFinal = Guid.NewGuid().ToString()[..6].ToLower();
        
        //  Garante que o aleatório não colida
        while (urlDatabase.ContainsKey(codigoFinal))
        {
            codigoFinal = Guid.NewGuid().ToString()[..6].ToLower();
        }
    }

    //  Salva no dictionary
    urlDatabase[codigoFinal] = dados.UrlOriginal;

    // Monta resposta
    string urlEncurtada = $"{context.Request.Scheme}://{context.Request.Host}/{codigoFinal}";

    var conteudoArquivo = $@"--- INFORMAÇÕES DO ENCURTADOR PRO ---
    Nome: {dados.Nome}
    Idade: {dados.Idade}
    URL Original: {dados.UrlOriginal}
    URL Encurtada: {urlEncurtada}
    Tipo: {(string.IsNullOrWhiteSpace(dados.CodigoSugerido) ? "Aleatório" : "Personalizado")}
    -------------------------------------";
    
    return Results.Ok(new {
        UrlEncurtada = urlEncurtada,
        ConteudoTexto = conteudoArquivo
    });
});

// Rota do redirecionamento 
app.MapGet("/{codigo}", (string codigo) =>
{
    if (urlDatabase.TryGetValue(codigo.ToLower(), out var urlOriginal))
    {
        return Results.Redirect(urlOriginal);
    }
    return Results.NotFound("URL não encontrada.");
});

app.Run();

class DadosUsuarioRequest
{
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
    public string UrlOriginal { get; set; } = string.Empty;
    public string? CodigoSugerido { get; set; } // Opcional
}