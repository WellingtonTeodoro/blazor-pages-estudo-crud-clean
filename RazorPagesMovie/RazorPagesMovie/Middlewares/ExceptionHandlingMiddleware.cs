using RazorPagesMovie.Domain.Exceptions;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ConcurrencyConflictException ex)
        {
            context.Items["ErrorMessagem"] = "Conflito de Concorrência: " + ex.Message;
            context.Response.Redirect("/Erro");
        }
        catch (Exception ex)
        {
            context.Items["ErrorMessage"] = "Erro Inesperado: " + ex.Message;
            context.Response.Redirect("/Erro");
        }
    }
}
