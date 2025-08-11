using Asp.Versioning;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RazorPagesMovie.Web.DependencyInjection;

public static class WebServiceCollectionExtensions
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        // 1. Configuração do Versionamento de API
        var apiVersioningBuilder = services.AddApiVersioning(options => // Captura o IApiVersioningBuilder
        {
            options.DefaultApiVersion = new ApiVersion(1, 0); //Aqui 'ApiVersion' é do Asp.Versioning
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        });

        // 2. API Explorer com versionamento - AGORA CHAMADO NO apiVersioningBuilder
        apiVersioningBuilder.AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        // 3. Swagger/Swashbuckle
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, Configuration.ConfigureSwaggerOptions>();
        services.AddSwaggerGen();

        // Outros serviços
        services.AddControllers();
        services.AddRazorPages();

        services.AddCors(options =>
        {
            options.AddPolicy("PermiteTudo", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

        services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
        services.AddSingleton<ITempDataDictionaryFactory, TempDataDictionaryFactory>();

        return services;
    }
}

/*✅ S — Single Responsibility Principle(SRP)
A classe está fazendo só o que ela deve fazer: registrar serviços da camada Web.Não mistura lógica de domínio, infraestrutura ou qualquer outra coisa.

✅ O — Open/Closed Principle(OCP)
Está aberta para extensão e fechada para modificação: se quiser registrar mais serviços, tem que estender esse método ou adicionar novos métodos de extensão — não precisa alterar o que já está funcionando.

✅ L — Liskov Substitution Principle (LSP)
Aqui não se aplica diretamente, já que estamos lidando com injeção de dependência, não com hierarquias de classes/subclasses.

✅ I — Interface Segregation Principle (ISP)
Não força nenhuma classe a depender de métodos que não usa. Cada serviço registrado segue o contrato que precisa, e ponto. Exemplo: ITempDataProvider, IApiVersioningBuilder, IConfigureOptions<SwaggerGenOptions>, etc.

✅ D — Dependency Inversion Principle (DIP)
Não instancia nada manualmente. Tudo é injetado via interfaces, como deve ser.O Swashbuckle, Versionamento e CORS são configurados declarativamente e injetados por contrato.*/