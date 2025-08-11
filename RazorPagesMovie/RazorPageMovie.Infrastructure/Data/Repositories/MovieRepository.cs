using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Domain.Entities;
using RazorPagesMovie.Domain.Interfaces;
using RazorPagesMovie.Infrastructure.Data.DbContexts;

namespace RazorPagesMovie.Infrastructure.Data.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly RazorPagesMovieContext _context;

    public MovieRepository(RazorPagesMovieContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Movie movie)
    {
        _context.Movie.Add(movie);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var movieDelete = await _context.Movie.FindAsync(id);

        if (movieDelete != null)
        {
            _context.Movie.Remove(movieDelete);
            await _context.SaveChangesAsync();
        }
    } 

    public async Task<Movie?> GetByIdAsync(int id)
    {
        return await _context.Movie.FindAsync(id);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Movie.AnyAsync(e => e.Id == id);
    }

    public async Task UpdateAsync(Movie movie)
    {
        _context.Movie.Update(movie);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Movie>> GetAllAsync()
    {
        return await _context.Movie.ToListAsync();
    }
}

/*✅ S — Single Responsibility Principle (SRP)
A classe tem uma responsabilidade clara e única: operar a persistência dos dados de filmes no banco via EF Core.
Não mistura lógica de negócio ou apresentação. Atende SRP perfeitamente.

✅ O — Open/Closed Principle (OCP)
A classe pode ser estendida (por herança ou composição) para adicionar funcionalidades específicas (ex: cache, logs, transações especiais) sem alterar o código base. Está aberta para extensão, fechada para modificação.

✅ L — Liskov Substitution Principle (LSP)
Implementa a interface IMovieRepository, então pode ser substituída por outras implementações (ex: mock para testes, repositório em memória) sem quebrar o contrato esperado.
Respeita LSP.

✅ I — Interface Segregation Principle (ISP)
Depende da interface focada e específica IMovieRepository, sem forçar o consumidor a depender de métodos não utilizados.
Correta aplicação de ISP.

✅ D — Dependency Inversion Principle (DIP)
Depende da abstração (interface IMovieRepository) e a implementação recebe o contexto via injeção de dependência no construtor.
DIP respeitado.*/