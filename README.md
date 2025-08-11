# RazorPagesMovie

Aplicação web baseada básica em ASP.NET Core Razor Pages para gerenciamento de filmes, estruturada com Clean Architecture e princípios SOLID.

## Funcionalidades principais

- CRUD completo para filmes via API REST versionada.
- Banco de dados SQL Server com EF Core e migrações automáticas.
- Seed de dados para inicialização.
- Documentação Swagger multi-versão com segurança JWT.
- Tratamento global de erros via middleware.
- Configuração robusta para ambientes de desenvolvimento e produção.
- Suporte a localização em português do Brasil.
- Modularização e extensibilidade via métodos de extensão.

## Tecnologias

- ASP.NET Core 7+
- Entity Framework Core
- Swagger (Swashbuckle)
- Asp.Versioning para versionamento de API
- Injeção de dependência nativa
- Middleware customizados
- C# 11

## Como rodar

1. Clone o repositório.
2. Deverá ser criado um banco com o mesmo nome do que está no projeto, não altere o banco.
3. Configure a string de conexão no `appsettings.json`.
4. Execute o projeto. O banco será migrado e populado automaticamente.
5. Acesse a API via Swagger em `/swagger`.
6. Acesse as páginas Razor em `/Index`.
7. Você pode ver também o json dos arquivos salvos.

---

**Arquitetura e design**

- Program.cs limpo e seguindo SRP.
- Injeção de dependência com abstração via interfaces.
- Controle de versões da API via cabeçalhos e rotas.
- Configuração centralizada da documentação Swagger.
- Boas práticas de segurança e desempenho.

---

**Projeto em constante evolução, focado em qualidade, escalabilidade e boas práticas.**
