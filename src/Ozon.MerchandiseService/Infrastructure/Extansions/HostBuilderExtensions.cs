using System;
using System.IO;
using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Ozon.MerchandiseService.Domain.AggregateModels.EmployeeAggregate;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;
using Ozon.MerchandiseService.Infrastructure.Filters;
using Ozon.MerchandiseService.Infrastructure.Handlers;
using Ozon.MerchandiseService.Infrastructure.Interceptors;
using Ozon.MerchandiseService.Infrastructure.Repositories;
using Ozon.MerchandiseService.Infrastructure.StartupFilters;
using Ozon.MerchandiseService.Services.Interfaces;

namespace Ozon.MerchandiseService.Infrastructure.Extansions
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder AddInfrastructure(this IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IDbContext, MerchandiseContext>();
                services.AddScoped<IMerchIssueRepository, MerchIssueRepository>();
                services.AddScoped<IEmployeeRepository,EmployeeRepository>();
            
                services.AddScoped<IMerchandiseService, Services.MerchandiseService>();
           
                services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
                
                services.AddGrpc(options=> options.Interceptors.Add<LoggingInterceptor>());
                
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