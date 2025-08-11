using RazorPagesMovie.Domain.Entities; 

namespace RazorPagesMovie.Domain.Interfaces; 

public interface IMovieRepository
{ 
    Task AddAsync(Movie movie); 
    Task<Movie?> GetByIdAsync(int id);
    Task UpdateAsync(Movie movie);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<IEnumerable<Movie>> GetAllAsync();
}

/*✅ S — Single Responsibility Principle (SRP)
A interface é responsável exclusivamente por persistência de entidades Movie.
Nada de lógica de negócio, nada de DTOs, nada de serviços externos. Responsabilidade única e bem definida. 

✅ O — Open/Closed Principle (OCP)
Ela pode ser estendida com novos métodos (SearchByTitleAsync, GetByGenreAsync, etc.)
Sem necessidade de modificar os métodos existentes. Aberta para extensão, fechada para modificação. 

✅ L — Liskov Substitution Principle (LSP)
Qualquer implementação de IMovieRepository pode ser usada sem quebrar o comportamento esperado (ex: um mock em teste, ou uma versão in-memory). As assinaturas são claras e não impõem contratos ocultos. Comportamento substituível garantido. 

✅ I — Interface Segregation Principle (ISP)
Não há métodos desnecessários. Cada contrato tem propósito claro para lidar com persistência de filmes.
Se no futuro quiser separar leitura (IReadableMovieRepository) de escrita (IWritableMovieRepository), será fácil.
Interface enxuta e bem focada. 

✅ D — Dependency Inversion Principle (DIP)
Camadas superiores (Application, Controller) dependem dessa abstração, não da implementação concreta (MovieRepository).
Trocar a fonte de dados (Ex: Mongo, InMemory, API externa) não exige mudar a camada de aplicação.
Cumpre perfeitamente o DIP. */