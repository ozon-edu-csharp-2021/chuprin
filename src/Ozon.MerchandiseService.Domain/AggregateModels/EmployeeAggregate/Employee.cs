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
        private static int _employeesCount = 1;
        public long EmployeeId { get; private set; }
        
        public Employee(long employeeId)
        {
            Id = _employeesCount++;
            EmployeeId = employeeId;
        }
    }
}