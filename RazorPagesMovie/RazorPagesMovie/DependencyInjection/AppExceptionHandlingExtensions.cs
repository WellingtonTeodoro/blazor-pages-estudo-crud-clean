namespace RazorPagesMovie.Web.DependencyInjection;
public static class AppExceptionHandlingExtensions
{
    public static void UseCustomExceptionHandling(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
            app.UseDeveloperExceptionPage();
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }
    }
}
/*✅ S — Single Responsibility Principle (SRP)
A classe tem uma única função clara: configurar o pipeline de tratamento de exceções.
Não mistura com logs, response headers, ou redirecionamentos externos. Enxuta e direta.

✅ O — Open/Closed Principle (OCP)
Está aberta para extensão: se quiser adicionar logs, monitoramento ou uma página de erro custom (ex: UseExceptionHandler("/falha")), pode fazer em novos métodos. O que está aqui não precisa ser alterado para crescer. 

✅ L — Liskov Substitution Principle (LSP)
Não tem herança ou polimorfismo envolvidos, mas...
Se alguém criar um wrapper de WebApplication, ainda assim o método funcionaria sem quebrar o comportamento. 

✅ I — Interface Segregation Principle (ISP)
O método exige apenas o mínimo necessário: um WebApplication.
Não obriga a passar logger, config, nem settings externos. Foco total na responsabilidade.

✅ D — Dependency Inversion Principle (DIP)
Não depende de implementações concretas. Só usa abstrações já providas pelo ASP.NET Core (como app.Environment e UseExceptionHandler).
Seguindo o DIP certinho.*/