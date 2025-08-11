using RazorPagesMovie.Application.DTOs;
using RazorPagesMovie.Application.Interfaces;
using RazorPagesMovie.Domain.Entities;
using RazorPagesMovie.Domain.Exceptions;
using RazorPagesMovie.Domain.Interfaces;

namespace RazorPagesMovie.Application.UseCases;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;

    public MovieService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<MovieDTO> CreateMovieAsync(MovieDTO movieDto)
    {
        var movieEntity = new Movie(
            title: movieDto.Title,
            releaseDate: movieDto.ReleaseDate,
            genre: movieDto.Genre,
            price: movieDto.Price
        );

        await _movieRepository.AddAsync(movieEntity);

        // Depois de salvar, preencher o Id gerado no DTO para retorno
        movieDto.Id = movieEntity.Id;
        return movieDto;
    }

    public async Task<bool> DeleteMovieAsync(int id)
    {
        var exists = await _movieRepository.ExistsAsync(id);
        if (!exists)
            return false;

        await _movieRepository.DeleteAsync(id);
        return true;
    }

    public async Task<MovieDTO?> GetMovieByIdAsync(int id)
    {
        var movieEntity = await _movieRepository.GetByIdAsync(id);

        if (movieEntity == null)
            return null;

        return new MovieDTO
        {
            Id = movieEntity.Id,
            Title = movieEntity.Title,
            ReleaseDate = movieEntity.ReleaseDate,
            Genre = movieEntity.Genre,
            Price = movieEntity.Price
        };
    }

    public async Task<List<MovieDTO>> GetAllMoviesAsync()
    {
        var movies = await _movieRepository.GetAllAsync();

        // Mapear lista de entidades para lista de DTOs
        return movies.Select(m => new MovieDTO
        {
            Id = m.Id,
            Title = m.Title,
            ReleaseDate = m.ReleaseDate,
            Genre = m.Genre,
            Price = m.Price
        }).ToList();
    }

    public async Task<bool> MovieExistsAsync(int id)
    {
        return await _movieRepository.ExistsAsync(id);
    }

    public async Task<bool> UpdateMovieAsync(MovieDTO movieDto)
    {
        var movieToUpdate = await _movieRepository.GetByIdAsync(movieDto.Id);

        if (movieToUpdate == null)
            return false;

        movieToUpdate.UpdateDetails(
            newTitle: movieDto.Title,
            newReleaseDate: movieDto.ReleaseDate,
            newGenre: movieDto.Genre,
            newPrice: movieDto.Price
        );

        try
        {
            await _movieRepository.UpdateAsync(movieToUpdate);
            return true;
        }
        catch (ConcurrencyConflictException ex)
        {
            if (!await _movieRepository.ExistsAsync(movieDto.Id))
                return false;

            throw new ConcurrencyConflictException(
                "O filme foi modificado por outro usuário. Por favor, revise suas alterações e tente novamente.",
                ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Ocorreu um erro inesperado ao atualizar o filme.", ex);
        }
    }
}

/*✅ S — Single Responsibility Principle (SRP)
A responsabilidade da classe é clara: orquestrar operações da entidade Movie no contexto da aplicação (criar, buscar, atualizar e deletar), lidando com DTOs e repositórios. Sem poluir com lógica de UI, infra ou banco. Respeita o SRP perfeitamente.

✅ O — Open/Closed Principle (OCP)
A lógica está bem encapsulada. Você pode adicionar, por exemplo, um método GetMoviesByGenreAsync() ou ApplyDiscountAsync() sem mexer no core já validado. O uso de DTOs facilita integração com outras camadas sem precisar mudar os métodos internos.
Expandível sem quebrar o que já funciona. OCP ok.

✅ L — Liskov Substitution Principle (LSP)
Ela depende apenas de interfaces (IMovieRepository, IMovieService). Qualquer implementação alternativa que siga os contratos vai funcionar aqui. Substituível, estável. LSP firme.

✅ I — Interface Segregation Principle (ISP)
Não força dependências desnecessárias. Recebe apenas o que realmente precisa (IMovieRepository).
Os métodos da interface IMovieService são coesos e separados. Nenhum cheiro de violação. ISP ok.

✅ D — Dependency Inversion Principle (DIP)
Ela depende da abstração IMovieRepository, não da implementação concreta (MovieRepository).
As entidades também são puras — sem acoplamento com infraestrutura. A inversão está respeitada com elegância. DIP ok.*/