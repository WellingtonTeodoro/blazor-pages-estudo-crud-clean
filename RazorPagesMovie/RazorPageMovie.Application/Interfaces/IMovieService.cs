using RazorPagesMovie.Application.DTOs;

namespace RazorPagesMovie.Application.Interfaces;

public interface IMovieService
{  
    Task<MovieDTO> CreateMovieAsync(MovieDTO movieDto);
    Task<MovieDTO?> GetMovieByIdAsync(int id);
    Task<List<MovieDTO>> GetAllMoviesAsync();
    Task<bool> UpdateMovieAsync(MovieDTO movieDto);
    Task<bool> DeleteMovieAsync(int id);
    Task<bool> MovieExistsAsync(int id);

}

/*✅ S — Single Responsibility Principle (SRP)
Essa interface define somente o contrato de operações sobre filmes (CRUD + verificação de existência).
Nada aqui trata de autenticação, logging, persistência direta ou regras genéricas.
Foco total em "serviço de filme".

✅ O — Open/Closed Principle (OCP)
Ela está aberta para extensão — se quiser adicionar, por exemplo, SearchMoviesByGenreAsync, você pode fazer isso sem alterar os métodos existentes. Sem riscos de quebrar o que já funciona. Extensível sem ser intrusiva. 

✅ L — Liskov Substitution Principle (LSP)
Qualquer classe que implemente essa interface (MovieService, no caso) pode ser usada no lugar dela.
Todos os métodos seguem assinatura clara, e não há pré-condições estranhas ou comportamento oculto. Substituível sem surpresas. 

✅ I — Interface Segregation Principle (ISP)
Ela só exige o que é necessário para quem vai trabalhar com filmes.
Nada de métodos genéricos inúteis nem acoplamento com DTOs desnecessários.
Se, no futuro, você quiser quebrar em ICreateMovieService, IReadMovieService etc., isso também seria válido — mas assim como está, está concisa e equilibrada. Não força nada além do que precisa. 

✅ D — Dependency Inversion Principle (DIP)
Ela é a abstração principal que outras camadas consomem (Controllers, Middlewares, etc).
A implementação concreta (MovieService) fica desacoplada e testável. Se quiser trocar a implementação amanhã por uma mockada, externa, etc., tá fácil. 100% DIP-friendly. */