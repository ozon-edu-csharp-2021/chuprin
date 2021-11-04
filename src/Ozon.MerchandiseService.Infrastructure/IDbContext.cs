using System.Collections.Generic;
using Ozon.MerchandiseService.Domain.AggregateModels.EmployeeAggregate;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;

namespace Ozon.MerchandiseService.Infrastructure.Repositories
{
    public interface IDbContext
    {
        List<Employee> Employees { get; }
        List<MerchIssue> MerchIssues { get; }
    }
}