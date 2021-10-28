using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Ozon.MerchandiseService.Infrastructure.Filters;
using Ozon.MerchandiseService.Infrastructure.StartupFilters;

namespace Ozon.MerchandiseService.Infrastructure.Extansions
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder AddInfrastructure(this IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddGrpc();
                services.AddControllers(options=>options.Filters.Add<GlobalExceptionFilter>());
                
                services.AddSingleton<IStartupFilter, TerminalStartupFilter>();
                services.AddSingleton<IStartupFilter, SwaggerStartupFilter>();
                
                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo {Title = "MerchandiseService", Version = "v1"});
                });
            });

            return builder;
        }
    }
}