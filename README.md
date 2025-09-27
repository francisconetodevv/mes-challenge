# MES - Sistema de Rastreabilidade

Sistema de Rastreabilidade para Manufatura (Manufacturing Execution System) desenvolvido em .NET 9.0.

## Estrutura do Projeto

O projeto está organizado em uma arquitetura em camadas:

- **MES.Rastreabilidade.Api**: Camada de API REST que expõe os endpoints do sistema
- **MES.Rastreabilidade.Core**: Camada de domínio contendo as entidades e regras de negócio
- **MES.Rastreabilidade.Infrastructure**: Camada de infraestrutura com implementações de persistência e serviços externos

## Tecnologias Utilizadas

- .NET 9.0
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger/OpenAPI

## Pré-requisitos

- .NET SDK 9.0 ou superior
- SQL Server (Local ou Express)
- Visual Studio 2022 ou VS Code com C# Dev Kit

## Como Executar

1. Clone o repositório
2. Configure a string de conexão no arquivo `appsettings.json` da API
3. Execute as migrações do Entity Framework:
```powershell
dotnet ef database update
```
4. Execute o projeto:
```powershell
dotnet run --project src/MES.Rastreabilidade.Api/MES.Rastreabilidade.Api.csproj
```

A API estará disponível em `https://localhost:7001` e você pode acessar a documentação Swagger em `https://localhost:7001/swagger`.

## Desenvolvimento

Para adicionar uma nova migração após alterar modelos:

```powershell
dotnet ef migrations add NomeDaMigracao -p src/MES.Rastreabilidade.Infrastructure
```

## Estrutura da Solução

```
src/
├── MES.Rastreabilidade.Api/        # Projeto da API
├── MES.Rastreabilidade.Core/       # Projeto de domínio
└── MES.Rastreabilidade.Infrastructure/  # Projeto de infraestrutura
```

## Licença

Este projeto está sob a licença [MIT](LICENSE).