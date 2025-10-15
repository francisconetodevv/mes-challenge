# Manufacturing Execution System (MES) - Rastreabilidade

Este é um sistema de Manufatura (MES) focado em rastreabilidade de produtos e ordens de produção, desenvolvido em .NET 9.0.

## Sobre o Projeto

O projeto é uma API REST desenvolvida em C# com .NET 9.0, seguindo os princípios de Clean Architecture. Ele é responsável por gerenciar produtos e ordens de produção em um ambiente de manufatura.

## Estrutura do Projeto

O projeto está dividido em três camadas principais:

- **MES.Rastreabilidade.Api**: Camada de apresentação que contém os controllers e configurações da API
- **MES.Rastreabilidade.Core**: Camada de domínio que contém as entidades e regras de negócio
- **MES.Rastreabilidade.Infrastructure**: Camada de infraestrutura que contém a implementação do banco de dados e migrações

## Entidades e Relacionamentos

### Produto
```csharp
public class Produto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
}
```

### Ordem de Produção (ProductionOrder)
```csharp
public class ProductionOrder 
{
    public int Id { get; set; }
    public string OrderCode { get; set; }
    public decimal QtyPlanned { get; set; }
    public ProductionOrderStatus Status { get; set; }
    public DateTime DateCreated { get; set; }
    public int ProductId { get; set; }
    public Produto Produto { get; set; }
}
```

### Relacionamentos
- Uma Ordem de Produção (ProductionOrder) pertence a um Produto (relação N:1)
- Um Produto pode ter múltiplas Ordens de Produção (relação 1:N)

### Status da Ordem de Produção
Uma ordem de produção pode ter os seguintes status:
- Planned (Planejada)
- OnGoing (Em Andamento)
- Finished (Finalizada)
- Canceled (Cancelada)

## Tecnologias Utilizadas

- .NET 9.0
- Entity Framework Core
- SQL Server
- Clean Architecture
- RESTful API

## Pré-requisitos

- .NET 9.0 SDK
- SQL Server
- Visual Studio 2022+ ou Visual Studio Code

## Como Executar

1. Clone o repositório
2. Navegue até a pasta do projeto
3. Restaure os pacotes NuGet:
```
dotnet restore
```
4. Atualize o banco de dados com as migrações:
```
dotnet ef database update
```
5. Execute o projeto:
```
dotnet run --project MES.Rastreabilidade.Api
```

## Endpoints da API

A API possui endpoints para gerenciar produtos e ordens de produção. A documentação detalhada dos endpoints está disponível através do Swagger quando a aplicação está em execução (rota: `/swagger`).

## Contribuindo

1. Faça um fork do projeto
2. Crie sua branch de feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request