using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ozon.MerchandiseService.Domain.AggregateModels.EmployeeAggregate;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;
using Ozon.MerchandiseService.GrpcService;
using Ozon.MerchandiseService.Infrastructure;
using Ozon.MerchandiseService.Infrastructure.Commands;
using Ozon.MerchandiseService.Infrastructure.Filters;
using Ozon.MerchandiseService.Infrastructure.Handlers;
using Ozon.MerchandiseService.Infrastructure.Middlewares;
using Ozon.MerchandiseService.Infrastructure.Repositories;
using Ozon.MerchandiseService.Infrastructure.StartupFilters;
using Ozon.MerchandiseService.Services.Interfaces;

namespace Ozon.MerchandiseService
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<MerchandiseGrpcService>();
                endpoints.MapControllers();
            });
        }
    }
}