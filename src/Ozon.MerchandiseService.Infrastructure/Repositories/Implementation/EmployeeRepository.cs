using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using Ozon.MerchandiseService.Domain.AggregateModels.EmployeeAggregate;
using Ozon.MerchandiseService.Infrastructure.Repositories.Interfaces;

namespace Ozon.MerchandiseService.Infrastructure.Repositories.Implementation
{
    public class EmployeeRepository: IEmployeeRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IQueryExecutor _queryExecutor;
        private readonly IChangeTracker _changeTracker;
        private const int Timeout = 5;


        public EmployeeRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory, IQueryExecutor queryExecutor, IChangeTracker changeTracker)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _queryExecutor = queryExecutor;
            _changeTracker = changeTracker;
        }

        public async Task<Employee> Add(Employee employee, CancellationToken token)
        {
            const string sql = @"
                INSERT INTO employees (employee_id)
                VALUES (@EmployeeId) RETURNING id;";

            var parameters = new
            {
                EmployeeId = employee.EmployeeId
            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: token);
            var connection = await _dbConnectionFactory.CreateConnection(token);
            
            var id = await connection.QuerySingleOrDefaultAsync<long>(commandDefinition);
            if (id != default)
                employee.SetId(id);
            _changeTracker.Track(employee);
            
            return employee;
        }

        public async Task<Employee> FindByEmployeeId(long id, CancellationToken token)
        {
            const string sql = @"
                SELECT id, employee_id
                FROM employees
                WHERE employees.employee_id = @EmployeeId;";
            
            var parameters = new
            {
                EmployeeId = id
            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: token);
            var connection = await _dbConnectionFactory.CreateConnection(token);
            var result = (await connection.QueryAsync<Models.Employee>(commandDefinition)).FirstOrDefault();
            
            if (result != null)
            {
                var employee = new Employee(result.Id, result.EmployeeId);
                _changeTracker.Track(employee);
                return employee;
            }

            return null;
        }
    }
}