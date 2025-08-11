using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace RazorPagesMovie.Infrastructure.Data.DbContexts;

public static class MigrationExtensions
{
    public static IHost MigrateDatabase<TContext>(this IHost host) where TContext : RazorPagesMovieContext
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<TContext>();
                // Garante que o banco de dados existe e aplica todas as migrações pendentes.
                // Se o DB não existir, ele será criado aqui (se o usuário tiver permissão).
                context.Database.Migrate();

                // Opcional: Se você tiver um SeedData para popular o banco de dados
                // Exemplo: SeedData.Initialize(services); 
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<TContext>>();
                logger.LogError(ex, "Ocorreu um erro durante a migração do banco de dados.");
                // Em um ambiente de produção, você pode querer re-lançar a exceção
                // ou ter um tratamento mais robusto. Para dev, logar é suficiente.
            }
        }
        return host;
    }
}
