using System;
using System.Collections.Generic;
using System.Linq;
using Castle.DynamicProxy;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;
using Ozon.MerchandiseService.Domain.Exceptions;
using Ozon.MerchandiseService.Domain.Seedwork;

namespace Ozon.MerchandiseService.Domain.AggregateModels.EmployeeAggregate
{
    public class Employee: Entity
    {
        public long EmployeeId { get; private set; }
        
        public Employee(long employeeId)
        {
            EmployeeId = employeeId;
        }

        public Employee(long id, long employeeId)
        {
            Id = id;
            EmployeeId = employeeId;
        }

        public void SetId(long id)
        {
            Id = id;
        }
    }
}