using RazorPagesMovie.Application.Interfaces;

namespace RazorPagesMovie.Web.DependencyInjection;
public static class AppDatabaseSeedExtensions
{
    public static async Task SeedDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var seedService = services.GetRequiredService<ISeedService>();
            await seedService.SeedMoviesAsync();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>(); // Ajuste aqui para ILogger<Program>
            logger.LogError(ex, "Erro ao popular o banco de dados.");
        }
    }
}

/*✅ S — Single Responsibility Principle (SRP)
A classe faz somente uma coisa: executa a semeadura (seed) de dados da aplicação.
Não mistura migração, limpeza ou verificação de integridade. Cumpre SRP com precisão.

✅ O — Open/Closed Principle (OCP)
Está aberta para extensão: quer fazer SeedUsersAsync() ou SeedProdutosAsync()?
Basta expandir a interface ISeedService, ou criar outro método de extensão.
O método atual não precisa ser alterado para isso. 

✅ L — Liskov Substitution Principle (LSP)
Não viola nenhuma expectativa de comportamento.
Se ISeedService for substituído por um mock ou stub, o método ainda roda e respeita contratos esperados. 

✅ I — Interface Segregation Principle (ISP)
O método depende somente de ISeedService e ILogger<Program>, que são exatamente o que ele precisa.
Não força nenhuma dependência desnecessária. Conciso e focado.

✅ D — Dependency Inversion Principle (DIP)
Totalmente aderente. Depende da abstração ISeedService, e resolve via injeção de dependência.
Nenhuma referência concreta, nenhuma instância direta.*/