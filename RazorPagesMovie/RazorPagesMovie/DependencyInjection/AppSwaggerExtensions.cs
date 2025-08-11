using Asp.Versioning.ApiExplorer;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace RazorPagesMovie.Web.DependencyInjection;
public static class AppSwaggerExtensions
{
    public static void UseSwaggerAndUI(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
            options.RoutePrefix = "swagger";
            options.DocExpansion(DocExpansion.None);
        });
    }
}

/*✅ S — Single Responsibility Principle(SRP)
A classe AppSwaggerExtensions tem uma única responsabilidade: configurar e aplicar Swagger + SwaggerUI na aplicação.Nada de misturar com versionamento, middlewares ou banco de dados.

✅ O — Open/Closed Principle(OCP)
Ela permite extensão via novos métodos(ex: UseSwaggerWithAuth(), UseSwaggerForMobile(), etc) sem alterar o que já está funcionando.

✅ L — Liskov Substitution Principle(LSP)
Não se aplica muito aqui, mas o uso de IApiVersionDescriptionProvider é correto e seguro.

✅ I — Interface Segregation Principle(ISP)
Não força ninguém a usar dependências desnecessárias.O método espera apenas WebApplication, e resolve tudo via DI.Conciso.

✅ D — Dependency Inversion Principle(DIP)
Não depende de concretudes. Pega IApiVersionDescriptionProvider da injeção de dependência (DI) — que já foi registrado via AddApiExplorer() corretamente antes.*/

