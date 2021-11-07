using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ozon.MerchandiseService.Domain.AggregateModels.EmployeeAggregate;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;
using Ozon.MerchandiseService.Domain.Seedwork;
using Ozon.MerchandiseService.Domain.SeedWork;

namespace Ozon.MerchandiseService.Infrastructure.Repositories
{
    public class MerchandiseContext : IDbContext
    {
        private readonly IMediator _mediator;
        public List<Employee> Employees { get; private set; }
        public List<MerchIssue> MerchIssues { get; private set; }
        public async Task Save()
        {
            await SendListDomainEvents(Employees);
            await SendListDomainEvents(MerchIssues);
        }

        private async Task SendListDomainEvents(IEnumerable<Entity> entities)
        {
            List<INotification> domainEvents = new List<INotification>();
            foreach (var entity in entities)
            {
                if (entity.DomainEvents != null)
                {
                    foreach (var domainEvent in entity.DomainEvents)
                        domainEvents.Add(domainEvent);
                
                    entity.ClearDomainEvents();
                }
            }

            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent);
            }
        }

        public MerchandiseContext(IMediator mediator)
        {
            _mediator = mediator;
            Employees = new List<Employee>();
            MerchIssues = new List<MerchIssue>();
        }

        
        
        /*private void Init()
        {
            Employees = new List<Employee>();
            MerchIssues = new List<MerchIssue>();

            Random rnd = new Random();
            for (int i = 1; i <= 100; i++)
            {
                Employees.Add(new Employee(i));
            }

            for (int i = 1; i <= 100; i++)
            {
                MerchIssues.Add(new MerchIssue(
                    rnd.Next(0, 100),
                    new MerchType() {Value = Enumeration.FromValue<MerchTypeEnum>(rnd.Next(1, 6))},
                    DateTime.Now
                ));
            }
        }*/
    }
}