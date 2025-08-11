using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace RazorPagesMovie.Web.DependencyInjection;
public static class AppLocalizationExtensions
{
    public static void UseRequestLocalization(this WebApplication app, params string[] cultures)
    {
        var supportedCultures = cultures.Select( c => new CultureInfo(c)).ToList();
        var localizationOptions = new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture(supportedCultures.First()),
            SupportedCultures = supportedCultures,
            SupportedUICultures = supportedCultures
        };
        app.UseRequestLocalization(localizationOptions);
    }
}

/*✅ S — Single Responsibility Principle (SRP)
A classe AppLocalizationExtensions tem uma única responsabilidade: configurar a localização da aplicação.
Não mistura com DI, middleware custom, nem configurações de cultura via arquivos externos.

✅ O — Open/Closed Principle (OCP)
Ela é aberta para extensão (ex: poderia ter UseBrazilianCulture() ou UseMultiLangFallback()),
mas fechada para modificação do que já está funcionando.

✅ L — Liskov Substitution Principle (LSP)
Não se aplica diretamente (não há herança envolvida), mas a assinatura é clara, e o uso de params string[] cultures é seguro e coerente com o esperado. Não quebra comportamento. 

✅ I — Interface Segregation Principle (ISP)
Não força nada além do necessário: só exige um WebApplication e uma lista de culturas.
Não polui a interface nem obriga dependências inúteis. Conciso e enxuto.

✅ D — Dependency Inversion Principle (DIP)
Não depende de implementações concretas.
Usa somente APIs do .NET (CultureInfo, RequestLocalizationOptions, etc).
Se algum dia precisar injetar configuração externa de cultura, é fácil de adaptar mantendo o DIP.*/