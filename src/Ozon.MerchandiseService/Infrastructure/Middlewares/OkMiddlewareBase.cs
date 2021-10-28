using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Ozon.MerchandiseService.Infrastructure.Middlewares
{
    internal class OkMiddlewareBase
    {
        public OkMiddlewareBase(RequestDelegate next) {}

        public virtual async Task InvokeAsync(HttpContext context)
        {
            await context.Response.WriteAsync("200 OK");
        }
    }
}