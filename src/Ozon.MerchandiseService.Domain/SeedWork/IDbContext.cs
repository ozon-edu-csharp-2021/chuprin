using System.Collections.Generic;
using System.Threading.Tasks;
using Ozon.MerchandiseService.Domain.AggregateModels.EmployeeAggregate;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;

namespace Ozon.MerchandiseService.Domain.Seedwork
{
    public interface IDbContext
    {
        List<Employee> Employees { get; }
        List<MerchIssue> MerchIssues { get; } 
        Task Save();
    }
}