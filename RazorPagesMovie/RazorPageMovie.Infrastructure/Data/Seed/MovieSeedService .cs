using RazorPagesMovie.Application.Interfaces;  
using RazorPagesMovie.Domain.Entities;
using RazorPagesMovie.Domain.Interfaces;        

namespace RazorPagesMovie.Infrastructure.Data.Seed;

public class MovieSeedService : ISeedService
{
    private readonly IMovieRepository _movieRepository;

    public MovieSeedService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task SeedMoviesAsync()
    {
        // Verifica se já existem filmes usando o repositório
        // Para isso, IMovieRepository precisa de um método para verificar a existência geral
        // ou você pode tentar adicionar e lidar com a exceção se já existir.
        // A forma mais direta é adicionar um método GetAllAsync ou AnyAsync no IMovieRepository.

        // Exemplo: Se você tiver um método GetAllAsync na IMovieRepository
        // if ((await _movieRepository.GetAllAsync()).Any())
        // {
        //     return; // Já populado
        // }

        // Ou, uma abordagem mais simples para seed:
        // Apenas tente adicionar. Se o banco de dados já existir e tiver dados,
        // você precisaria de uma lógica mais sofisticada ou de um Identity Seed para chaves únicas.
        // Para um seed inicial simples, verificar se há algum filme é OK.

        // Se seu IMovieRepository tem um ExistsAsync(int id) ou similar,
        // você pode adaptar para ExistsAnyAsync() ou GetCountAsync().
        // Por agora, vamos assumir que o repositório sabe como verificar se está vazio.

        // Para simplificar e evitar um método 'Any' no repo só para seed,
        // vamos adicionar um TryAdd, ou simplesmente assumir que ele só será rodado uma vez.
        // Se você tiver uma constraint de unicidade no título, AddAsync lançaria uma exceção.

        // Uma abordagem mais robusta seria buscar por título antes de adicionar, ou usar GetAll.
        // Vamos adicionar um método GetAllAsync na IMovieRepository e usá-lo.
        if ((await _movieRepository.GetAllAsync()).Any()) 
        {
            return; // O banco de dados já foi populado com filmes.
        }

        var moviesToSeed = new List<Movie>
        {
            new Movie(
                title: "O Exterminador do Futuro",
                releaseDate: DateTime.Parse("1988-10-15"),
                genre: "Ação",
                price: 25.00M
            ),
            new Movie(
                title: "Matrix",
                releaseDate: DateTime.Parse("1999-10-01"),
                genre: "Ação/Futurista",
                price: 20.00M
            )
        };

        foreach (var movie in moviesToSeed)
        {
            await _movieRepository.AddAsync(movie);
        }
        // O SaveChanges é feito dentro do AddAsync no MovieRepository, então não precisa aqui.
    }
}

/*✅ S — Single Responsibility Principle (SRP)
A classe tem uma única responsabilidade: popular o banco com dados iniciais de filmes.
Não mistura lógica de domínio, serviço ou apresentação. Perfeito para SRP.

✅ O — Open/Closed Principle (OCP)
Pode ser estendida para adicionar mais filmes ou ajustar a lógica de seed sem modificar o comportamento base.
Caso precise mudar como os dados são carregados (ex: ler de arquivo), pode-se herdar ou compor sem alterar a classe.
Atende OCP.

✅ L — Liskov Substitution Principle (LSP)
Depende da abstração IMovieRepository e respeita contratos esperados, podendo ser substituída por outra implementação do repositório.
Nenhuma violação visível. LSP respeitado.

✅ I — Interface Segregation Principle (ISP)
Usa apenas a interface IMovieRepository focada em repositório de filmes.
Não força a depender de métodos que não usa. Correto com ISP.

✅ D — Dependency Inversion Principle (DIP)
Depende de abstrações (IMovieRepository), não concretos.
Injeta dependência via construtor, desacoplando a implementação da classe de seed.
Seguindo DIP com excelência.*/