using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Ozon.MerchandiseService.Infrastructure.Middlewares
{
    internal sealed class LiveMiddleware: OkMiddlewareBase
    {
        public LiveMiddleware(RequestDelegate next):base(next)  {}
        public override Task InvokeAsync(HttpContext context)
        {
            return base.InvokeAsync(context);
        }
    }
}