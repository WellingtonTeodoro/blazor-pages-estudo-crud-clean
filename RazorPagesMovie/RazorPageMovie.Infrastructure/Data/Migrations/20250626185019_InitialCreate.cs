using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorPagesMovie.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Movie",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                Genre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Movie", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Movie");
    }
}

/*✅ S — Single Responsibility Principle (SRP)
Responsável apenas por criar e remover a tabela Movie no banco. Nenhuma lógica adicional. Atende SRP.

✅ O — Open/Closed Principle (OCP)
Classe específica da migração, não deve ser alterada após criada, mas pode ser estendida criando novas migrations.
Ok para OCP.

✅ L — Liskov Substitution Principle (LSP)
Herdar de Migration é o esperado e não quebra contrato algum. Atende LSP.

✅ I — Interface Segregation Principle (ISP)
Não implementa interfaces, mas não força dependências desnecessárias. Neutro.

✅ D — Dependency Inversion Principle (DIP)
Não depende de implementações concretas, apenas da abstração MigrationBuilder fornecida pelo EF Core. Ok.*/