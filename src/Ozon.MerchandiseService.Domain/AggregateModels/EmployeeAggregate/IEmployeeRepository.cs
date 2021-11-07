using System.Threading.Tasks;
using Ozon.MerchandiseService.Domain.Seedwork;

namespace Ozon.MerchandiseService.Domain.AggregateModels.EmployeeAggregate
{
    public interface IEmployeeRepository
    {
        void Add(Employee employee);
        Employee GetById(long id);
        IDbContext UnitOfWork { get; }
    }
}