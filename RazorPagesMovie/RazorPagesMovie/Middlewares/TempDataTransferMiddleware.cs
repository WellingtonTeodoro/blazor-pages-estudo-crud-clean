using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace RazorPagesMovie.Web.Middlewares
{
    public class TempDataTransferMiddleware
    {
        private readonly RequestDelegate _next;
        public TempDataTransferMiddleware(RequestDelegate next) { 
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Items.TryGetValue("ErrorMessage", out var errorObj) && errorObj is string error)
            {
                var factory = context.RequestServices.GetRequiredService<ITempDataDictionaryFactory>();
                var tempData = factory.GetTempData(context);
                tempData["ErrorMessage"] = error;
            }

            await _next(context);
        }
    }
}
