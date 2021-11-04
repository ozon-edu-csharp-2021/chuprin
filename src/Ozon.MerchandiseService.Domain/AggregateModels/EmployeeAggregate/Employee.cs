using System.Collections.Generic;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;
using Ozon.MerchandiseService.Domain.Seedwork;

namespace Ozon.MerchandiseService.Domain.AggregateModels.EmployeeAggregate
{
    public class Employee: Entity
    {
        public Employee(int id)
        {
            Id = id;
        }
    }
}