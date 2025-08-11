using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Infrastructure.Data.DbContexts;
using RazorPagesMovie.Infrastructure.DependencyInjection;
using RazorPagesMovie.Web.DependencyInjection;  
using RazorPagesMovie.Web.Middlewares; 

namespace RazorPagesMovie.Web;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Registro de serviços (chamada para WebServiceCollectionExtensions)
        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddWebServices(); 

        var app = builder.Build();

        // Garante que banco está migrado antes de rodar
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<RazorPagesMovieContext>();
            db.Database.Migrate();
        }

        // Ambiente de Desenvolvimento
        if (app.Environment.IsDevelopment())
        {
            // 2. Swagger e Swagger UI
            // Assumindo que UseSwaggerAndUI está em um namespace acessível
            app.UseSwaggerAndUI(); // Chama o método da nova classe (ex: AppSwaggerExtensions)
        }
        else // Ambiente de Produção
        { 
            app.UseHsts(); // HSTS para segurança
        }  

        // 3. Seed do Banco de Dados
        // Assumindo que SeedDatabaseAsync está em um namespace acessível
        await app.SeedDatabaseAsync(); // Chama o método da nova classe (ex: AppDatabaseSeedExtensions)

        // Middlewares Padrões do ASP.NET Core
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        // 4. Localização (colocado aqui para seguir a ordem comum dos middlewares)
        // Assumindo que UseRequestLocalization está em um namespace acessível
        app.UseRequestLocalization("pt-BR"); // Chama o método da nova classe (ex: AppLocalizationExtensions)

        app.UseRouting();
        app.UseCors("PermiteTudo"); // Certifique-se de que "PermiteTudo" está definido na política CORS

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage(); // Página de erro detalhada para desenvolvimento 
        }
        else // Ambiente de Produção
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>(); // Middleware customizado
            app.UseMiddleware<TempDataTransferMiddleware>(); // Middleware customizado
        }
        
        app.UseAuthorization();

        app.MapRazorPages();
        app.MapGet("/", context =>
        {
            context.Response.Redirect("/Index");
            return Task.CompletedTask;
        });
        app.MapControllers();

        app.Run();
    }
}

/*✅ S — Single Responsibility Principle (SRP)
Responsabilidade clara: configurar e iniciar a aplicação.
Toda lógica especializada (Swagger, Seed, Localization, Middlewares) foi extraída para extensions methods, deixando o Program.cs limpo e legível. SRP cumprido com louvor. 

✅ O — Open/Closed Principle (OCP)
Aberto para extensão via chamadas como:

app.UseSwaggerAndUI()

await app.SeedDatabaseAsync()

app.UseRequestLocalization("pt-BR")
Quer adicionar novos recursos? Cria outra extension e chama aqui.
Não precisa mudar o que já funciona. 

✅ L — Liskov Substitution Principle (LSP)
Não se aplica diretamente ao Program.cs, pois não há herança ou substituição envolvida aqui.
Mas o uso correto de abstrações (ex: IMovieService, ISeedService) em outros pontos garante compatibilidade com esse princípio.
Nada a corrigir. 

✅ I — Interface Segregation Principle (ISP)
Novamente, Program.cs não define interfaces diretamente.
Mas usa interfaces especializadas por baixo (via injeção de dependência nas extensions), como ILogger<Program>, IApiVersionDescriptionProvider, etc. Sem dependências desnecessárias, apenas o que importa. Está dentro. 

✅ D — Dependency Inversion Principle (DIP)
Toda dependência (repos, serviços, middlewares) é injetada via DI, configurada previamente em AddWebServices() ou AddInfrastructureServices(). O Program.cs não sabe quem implementa nada, apenas usa. Alta coesão, baixo acoplamento. */

/*🧠 Extras – Boas práticas aplicadas:
  🔄 Ambientes bem tratados: desenvolvimento e produção recebem pipelines diferentes.
  🧼 Separação de responsabilidades via namespaces: Web, Infrastructure, Middlewares, etc.
  🔐 Segurança: UseHsts(), UseHttpsRedirection() e CORS já configurados.
  🧩 Modularização extrema com métodos de extensão: deixa o Main() quase declarativo.*/