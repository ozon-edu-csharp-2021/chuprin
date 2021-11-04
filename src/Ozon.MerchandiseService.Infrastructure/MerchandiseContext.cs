using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ozon.MerchandiseService.Domain.AggregateModels.EmployeeAggregate;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;
using Ozon.MerchandiseService.Domain.Seedwork;
using Ozon.MerchandiseService.Domain.SeedWork;

namespace Ozon.MerchandiseService.Infrastructure.Repositories
{
    public class MerchandiseContext: IDbContext
    {
        public List<Employee> Employees { get; private set; }
        public List<MerchIssue> MerchIssues { get; private set; }
        
        public MerchandiseContext()
        {
            Init();
        }
        private void Init()
        {
            Employees = new List<Employee>();
            MerchIssues = new List<MerchIssue>();
            
            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                Employees.Add(new Employee(i));
            }

            for (int i = 0; i < 100; i++)
            {
                MerchIssues.Add(new MerchIssue(
                    i,
                   Employees[rnd.Next(0,100)],
                  new MerchType() {Value = Enumeration.FromValue<MerchTypeEnum>(rnd.Next(1,6))},
                  Enumeration.FromValue<IssueStatusEnum>(rnd.Next(1,4))
              ));
            }
        }
    }
}