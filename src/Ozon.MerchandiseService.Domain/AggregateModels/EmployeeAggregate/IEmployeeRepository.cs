using System.Threading.Tasks;

namespace Ozon.MerchandiseService.Domain.AggregateModels.EmployeeAggregate
{
    public interface IEmployeeRepository
    {
        void Add(Employee employee);
        Employee GetById(long id);
        int Count();
        void Save();
    }
}