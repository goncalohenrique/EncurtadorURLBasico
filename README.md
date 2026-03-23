# 🚀 Encurtador de URL Básico (Estudo de ASP.NET Core)

Este é um projeto experimental de um encurtador de URL desenvolvido com **ASP.NET Core (Minimal APIs)**. O objetivo principal foi explorar a agilidade e a simplicidade do ecossistema .NET para criar serviços web modernos e leves.

## 📌 Funcionalidades

- **Encurtamento Aleatório:** Gera um código único de 6 caracteres para sua URL de forma automática.
- **URL Personalizada:** O usuário pode escolher o "apelido"  do link, com validação de duplicidade no servidor.
- **Coleta de Dados:** O formulário solicita nome e idade, integrando essas informações no processamento da requisição.
- **Exportação de Dados:** Ao gerar o link, o sistema realiza o download automático de um arquivo `.txt` contendo os metadados da operação (Nome, Idade, URL Original e Encurtada).
- **Cópia Rápida:** Ícone interativo para copiar a URL encurtada para a área de transferência via Clipboard API com feedback visual.

## 🛠️ Tecnologias Utilizadas

- **Back-end:** C# com ASP.NET Core 9.0 (Minimal APIs).
- **Front-end:** HTML5, CSS e JavaScript Assíncrono (Fetch API).

## 💡 O que eu aprendi e impressões

Durante o desenvolvimento deste projeto, pude consolidar conceitos importantes de desenvolvimento Web e integração entre cliente e servidor.

### ⚡ .NET vs Spring Boot (Java)
Vindo de experiências anteriores com **Java e Spring Boot**, uma das maiores surpresas neste projeto foi a clareza e simplicidade do **ASP.NET Core**.
- Notei que o .NET permite chegar ao resultado final com muito menos "boilerplate" (código repetitivo).
- Enquanto no Spring Boot eu precisaria de diversas anotações, classes de configuração e uma estrutura de pastas rígida para um serviço simples, as **Minimal APIs** do .NET me permitiram subir um endpoint funcional de forma muito mais direta, intuitiva e produtiva.

## ⚠️ Observações de Projeto

> Este é um projeto estritamente para **fins de estudo**.
> - **Persistência:** Os dados **não são persistidos em banco de dados**. Por questões de simplicidade e foco no fluxo lógico, as URLs são armazenadas na memória RAM do servidor (Dictionary). Caso o serviço seja reiniciado, os links deixarão de funcionar.
> - **Segurança:** Não foram implementadas camadas de autenticação ou limites de requisição (rate limiting).

##  Como rodar o projeto localmente

1. Certifique-se de ter o [.NET SDK](https://dotnet.microsoft.com/download) instalado.
2. Clone o repositório:
   ```bash
   git clone [https://github.com/goncalohenrique/EncurtadorURLBasico.git](https://github.com/goncalohenrique/EncurtadorURLBasico.git)