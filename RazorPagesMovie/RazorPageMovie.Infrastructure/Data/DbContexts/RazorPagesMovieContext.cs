using Microsoft.EntityFrameworkCore;

namespace RazorPagesMovie.Infrastructure.Data.DbContexts;

public class RazorPagesMovieContext : DbContext
{
    public RazorPagesMovieContext (DbContextOptions<RazorPagesMovieContext> options)
        : base(options)
    {
    }

    public DbSet<RazorPagesMovie.Domain.Entities.Movie> Movie { get; set; } = default!;
}

/*✅ S — Single Responsibility Principle (SRP)
Responsável apenas por expor o DbSet<Movie> e configurar a conexão via DbContextOptions.
Não mistura lógica de negócio, nem responsabilidades de infraestrutura adicionais. Perfeito para SRP.

✅ O — Open/Closed Principle (OCP)
Aberto para extensão (ex: adicionar mais DbSets, OnModelCreating para configurar entidades), mas fechado para modificação no que já está funcionando. Segue bem o OCP.

✅ L — Liskov Substitution Principle (LSP)
Pode ser substituído por qualquer outro DbContext, inclusive mocks em testes, sem quebrar o contrato. Ok para LSP.

✅ I — Interface Segregation Principle (ISP)
Não implementa interface diretamente, mas não obriga ninguém a lidar com métodos ou membros que não vai usar.
Neutro, não viola o princípio.

✅ D — Dependency Inversion Principle (DIP)
Recebe DbContextOptions<RazorPagesMovieContext> via injeção de dependência — não instancia nada por conta própria.
Muito bem alinhado com DIP.*/