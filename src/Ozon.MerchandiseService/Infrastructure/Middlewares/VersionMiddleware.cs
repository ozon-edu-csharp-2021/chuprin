using System;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ozon.MerchandiseService.Infrastructure.Middlewares
{
    internal sealed class VersionMiddleware
    {
        public VersionMiddleware(RequestDelegate next) {}

        public async Task InvokeAsync(HttpContext context)
        {
            var versionText = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "no version";
            var serviceNameText = Assembly.GetExecutingAssembly().GetName().Name ?? "no name";

            var jsonResult = JsonSerializer.Serialize(new
            {
                version = versionText,
                serviceName = serviceNameText
            });
            
            await context.Response.WriteAsync(jsonResult);
        }
    }
}