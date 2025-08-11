using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RazorPagesMovie.Application.Interfaces;
using RazorPagesMovie.Application.UseCases;
using RazorPagesMovie.Domain.Interfaces;
using RazorPagesMovie.Infrastructure.Data.DbContexts;
using RazorPagesMovie.Infrastructure.Data.Repositories;
using RazorPagesMovie.Infrastructure.Data.Seed;

namespace RazorPagesMovie.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Configura o DbContext
            services.AddDbContext<RazorPagesMovieContext>(options =>
            {
                // Obtém a string de conexão do appsettings.json
                var connectionString = configuration.GetConnectionString("RazorPagesMovieContext")
                                       ?? throw new InvalidOperationException("Connection string 'RazorPagesMovieContext' not found.");

                // Configura o SQL Server como provedor do banco de dados
                options.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
                {
                    // **Opcional, mas recomendado: Habilita resiliência a falhas transitórias.**
                    // Isso faz com que o EF Core tente novamente em caso de erros temporários de conexão.
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 10, // Número de tentativas antes de falhar
                        maxRetryDelay: TimeSpan.FromSeconds(30), // Atraso máximo entre as tentativas
                        errorNumbersToAdd: null); // Deixe null para usar os números de erro padrão do SQL Server
                });
            });

            // Registra o repositório concreto (se você tiver interfaces de repositório no Domain)
            // Registro do Repositório
            services.AddScoped<IMovieRepository, MovieRepository>();

            // Registro do Serviço de Aplicação (UseCase) 
            services.AddScoped<IMovieService, MovieService>();

            // Registra o Seed
            services.AddScoped<ISeedService, MovieSeedService>();

            //Registra outros serviços de infraestrutura (ex: EmailSender)
            // services.AddTransient<IEmailSender, EmailSender>(); 

            return services;
        }
    }
}

/*✅ S — Single Responsibility Principle (SRP)
A classe tem uma responsabilidade única e clara: registrar todos os serviços de infraestrutura e configuração do banco no DI container. Não mistura responsabilidades de aplicação, UI ou domínio. Cumpre SRP.

✅ O — Open/Closed Principle (OCP)
Pode ser estendida adicionando mais registros no método AddInfrastructureServices (ex: mais repositórios, serviços, etc) sem alterar a estrutura básica da classe. Aberta para extensão, fechada para modificação.

✅ L — Liskov Substitution Principle (LSP)
Embora não tenha hierarquia clara aqui (classe estática de extensão), não há violações perceptíveis.
Registra abstrações (IMovieRepository, IMovieService) com suas implementações concretas (MovieRepository, MovieService), respeitando substituibilidade. LSP aplicado corretamente.

✅ I — Interface Segregation Principle (ISP)
Depende apenas das interfaces necessárias para a infraestrutura (IMovieRepository, IMovieService, ISeedService).
Não força consumidores a depender de métodos que não usam. Adequado ao ISP.

✅ D — Dependency Inversion Principle (DIP)
Registra interfaces para abstrações e injeta as dependências concretas necessárias.
Não usa concretos diretamente na aplicação, apenas na infraestrutura, isolando implementações.
Segue DIP corretamente.*/