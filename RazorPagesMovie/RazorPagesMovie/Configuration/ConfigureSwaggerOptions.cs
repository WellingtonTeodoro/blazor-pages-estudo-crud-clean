using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen; 

namespace RazorPagesMovie.Web.Configuration;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(
                description.GroupName,
                CreateVersionInfo(description));
        }

        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "Entre com 'Bearer ' [espaço] e então seu token. Ex: \"Bearer 12345abcdef\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            }
        });

        var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath))
        {
            options.IncludeXmlComments(xmlPath);
        }

        // Mantenha esta linha se você tiver ambiguidade de rotas
        options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    }

    private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
    {
        var info = new OpenApiInfo()
        {
            Title = "RazorPagesMovie API",
            Version = description.ApiVersion.ToString(),
            Description = "Uma API de exemplo para gerenciar filmes."
        };

        if (description.IsDeprecated)
        {
            info.Description += " **Esta versão da API está obsoleta.**";
        }

        return info;
    }
}

/*✅ S — Single Responsibility Principle (SRP)
A classe tem uma única responsabilidade clara: configurar as opções do Swagger para múltiplas versões da API, incluindo segurança, documentação XML, e resolução de conflitos. Nada além disso. Cumpre o SRP perfeitamente.

✅ O — Open/Closed Principle (OCP)
Pode ser estendida facilmente para adicionar novas configurações (ex: mais esquemas de segurança, customizações na documentação) sem alterar o código existente, apenas adicionando novos métodos ou modificando o construtor.
Está aberta para extensão e fechada para modificação.

✅ L — Liskov Substitution Principle (LSP)
Implementa IConfigureOptions<SwaggerGenOptions>, então pode ser usada em qualquer lugar onde essa interface for requerida.
Cumpre LSP sem problemas.

✅ I — Interface Segregation Principle (ISP)
Depende da interface IConfigureOptions<T>, focada na configuração do Swagger.
Não impõe dependências extras ou métodos desnecessários. Está adequada ao ISP.

✅ D — Dependency Inversion Principle (DIP)
Depende da abstração IApiVersionDescriptionProvider, que é injetada via construtor.
Não depende de implementações concretas. Segue DIP perfeitamente.*/