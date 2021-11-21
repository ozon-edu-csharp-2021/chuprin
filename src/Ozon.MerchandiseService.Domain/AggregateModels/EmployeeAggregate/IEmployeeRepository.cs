using System.Threading;
using System.Threading.Tasks;
using Ozon.MerchandiseService.Domain.Seedwork;

namespace Ozon.MerchandiseService.Domain.AggregateModels.EmployeeAggregate
{
    public interface IEmployeeRepository
    {
        Task<Employee> Add(Employee employee, CancellationToken token);
        Task<Employee> FindByEmployeeId(long id, CancellationToken token);
    }
}