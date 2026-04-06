# Rental Car

API REST para gerenciamento de locação de veículos, desenvolvida em C# com .NET.

## Funcionalidades

- Cadastro e consulta de veículos disponíveis
- Registro de locações por cliente
- Controle de disponibilidade de frota
- CRUD de clientes e veículos

## Tecnologias

- C# / ASP.NET Core
- Entity Framework Core
- SQL Server

## Como rodar

1. Clone o repositório
2. Configure a connection string no `appsettings.json`
3. Execute as migrations: `dotnet ef database update`
4. Execute: `dotnet run`
