# Autenticação JWT com ASP.NET Core Identity

Este projeto é uma API construída com ASP.NET Core (.NET 8) que implementa autenticação de usuários utilizando o ASP.NET Core Identity e geração de tokens JWT (JSON Web Tokens).

## ?? Tecnologias Utilizadas

- ASP.NET Core 8.0
- Entity Framework Core
- ASP.NET Core Identity
- JWT (JSON Web Token)
- Swashbuckle (Swagger)
- SQL Server

## ?? Funcionalidades

- Registro de usuários
- Login com autenticação JWT
- Proteção de rotas com `[Authorize]`
- Validação de token
- Geração de token assinado com chave simétrica

## ?? Requisitos

- .NET 8 SDK
- SQL Server (ou outro banco compatível com EF Core)
- Visual Studio 2022 / VS Code

## ?? Configuração

1. Clone o repositório:
   ```bash
   git clone https://github.com/seu-usuario/nomedorepositorio.git
   cd nomedorepositorio

2. Edite o arquivo appsettings.json com sua string de conexão e chave JWT segura:
   {
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=AuthDb;Trusted_Connection=True;"
  },
  "Jwt": {
    "Key": "sua-chave-super-segura-com-32-caracteres",
    "Issuer": "sua-api",
    "Audience": "sua-api"
  }
}

3. Aplique as migrations para criar o banco:
 dotnet ef database update

4. Rode o projeto:
dotnet run

5. Acesse a interface Swagger para testar:
http://localhost:5100/swagger

