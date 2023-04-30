

# Projeto CRUD ASP.NET Core 6 MVC com EF Core e MySQL

Este é um projeto básico que implementa operações CRUD (Criar, Ler, Atualizar e Excluir) usando o ASP.NET Core 6, EF Core e MySQL. O projeto permite ao usuário realizar operações CRUD em uma tabela "Clients" usando uma interface web.

## Requisitos do Sistema

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) ou [Visual Studio Code](https://code.visualstudio.com/)

## Configuração do Banco de Dados

Antes de executar o projeto, é necessário configurar o banco de dados MySQL.

1. Instale o MySQL Server e MySQL Workbench
2. Crie um banco de dados com o nome "dotnetcoremysql"
3. Edite a ConnectionString no arquivo "appsettings.json" para incluir as informações de conexão com o seu banco de dados MySQL.

```
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=DotNetCoreMySql;User=root;Password=coloqueSuaSenhaAqui;"
}
```

## Executando o Projeto

1. Abra o projeto no Visual Studio ou Visual Studio Code
2. Execute o comando `dotnet ef database update` no terminal para aplicar as migrações para o banco de dados
3. Execute o projeto usando o Visual Studio ou o comando `dotnet run` no terminal
4. Acesse o aplicativo em seu navegador usando a URL `http://localhost:5000/Clients`

## Funcionalidades

- Listagem de Clientes
- Adicionar um novo Cliente
- Editar um Cliente existente
- Excluir um Cliente existente

## Tecnologias Utilizadas

- ASP.NET Core 6 MVC
- Entity Framework Core
- MySQL

## Conclusão

Este é um projeto básico que demonstra a implementação de operações CRUD usando ASP.NET Core 6, EF Core e MySQL. O projeto pode ser utilizado como base para a criação de projetos mais complexos.
