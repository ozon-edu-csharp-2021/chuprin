using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ozon.MerchandiseService.Domain.AggregateModels.EmployeeAggregate;
using Ozon.MerchandiseService.Domain.Seedwork;
using Ozon.MerchandiseService.Infrastructure.Repositories;

namespace Ozon.MerchandiseService.Infrastructure
{
    public class EmployeeRepository: IEmployeeRepository
    {
        public readonly IDbContext _context;
        public IDbContext UnitOfWork
        {
            get { return _context; }
        }

        public EmployeeRepository(IDbContext context)
        {
            _context = context;
        }

        public void Add(Employee employee)
        {
            _context.Employees.Add(employee);
        }

        public Employee GetById(long id)
        {
            var employee = _context.Employees.FirstOrDefault(x => x.Id == id);
            return employee;
        }

        public int Count()
        {
            return _context.Employees.Count;
        }
    }
}