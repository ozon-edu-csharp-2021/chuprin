using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;
using Ozon.MerchandiseService.Infrastructure.Repositories.Interfaces;

namespace Ozon.MerchandiseService.Infrastructure.Repositories.Implementation
{
    public class MerchIssueRepository: IMerchIssueRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IQueryExecutor _queryExecutor;
        private readonly IChangeTracker _changeTracker;

        public MerchIssueRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory,
            IQueryExecutor queryExecutor, IChangeTracker changeTracker)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _queryExecutor = queryExecutor;
            _changeTracker = changeTracker;
        }

        private const int Timeout = 5;

        public async Task<MerchIssue> Add(MerchIssue merchIssue, CancellationToken token)
        {
            const string sql = @"
                insert into issues (employee_id, merch_type_id, date_create, status_id)
                VALUES (@EmployeeId, @MerchTypeId, @DateCreated, @StatusId) RETURNING id;";
            
            foreach (var item in merchIssue.MerchIssueItems.Where(x=>x.IssueStatus.Equals(IssueStatusEnum.IsCreated)))
            {
                var parameters = new
                {
                    EmployeeId = merchIssue.EmployeeId,
                    MerchTypeId = item.MerchPackType.Value.Id,
                    DateCreated = item.DateCreated,
                    StatusId = item.IssueStatus.Id
                };
                var commandDefinition = new CommandDefinition(
                    sql,
                    parameters: parameters,
                    commandTimeout: Timeout,
                    cancellationToken: token);
                var connection = await _dbConnectionFactory.CreateConnection(token);
                var id = await connection.QuerySingleOrDefaultAsync<long>(commandDefinition);
                if (id != default)
                {
                    item.SetId(id);
                }
            }

            _changeTracker.Track(merchIssue);

            return merchIssue;
        }

        public async Task<MerchIssue> GetById(int id, CancellationToken token)
        {
            const string sql = @"
                SELECT id, employee_id, merch_type_id, date_create, status_id
                FROM issues
                WHERE issues.id = @IssueId;";

            var parameters = new
            {
                IssueId = id
            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: token);
            var connection = await _dbConnectionFactory.CreateConnection(token);
            var result = await connection.QueryAsync<Models.MerchIssue>(commandDefinition);

            if (result.Count() == 0)
                return null;

            MerchIssue merchIssue = new MerchIssue(result.First().EmployeeId,
                result.Select(x => new MerchIssueItem(
                    x.Id,
                    new MerchType() {Value = MerchTypeEnum.From(x.MerchTypeId)},
                    x.DateCreate,
                    IssueStatusEnum.From(x.StatusId))).ToList());
            
            _changeTracker.Track(merchIssue);
            
            return merchIssue;
        }

        public async Task<MerchIssue> Update(MerchIssue merchIssue, CancellationToken token)
        {
            const string sql = @"
                UPDATE issues 
                SET employee_id = @EmployeeId,
                    merch_type_id = @MerchTypeId, 
                    date_create = @DateCreated, 
                    status_id = @StatusId
                WHERE id = @Id;";
            
            foreach (var item in merchIssue.MerchIssueItems)
            {
                var parameters = new
                {
                    EmployeeId = merchIssue.EmployeeId,
                    MerchTypeId = item.MerchPackType.Value.Id,
                    DateCreated = item.DateCreated,
                    StatusId = item.IssueStatus.Id,
                    Id = item.Id
                };
                var commandDefinition = new CommandDefinition(
                    sql,
                    parameters: parameters,
                    commandTimeout: Timeout,
                    cancellationToken: token);
                var connection = await _dbConnectionFactory.CreateConnection(token);
                var id = await connection.QuerySingleOrDefaultAsync<long>(commandDefinition);
                if (id != default)
                {
                    item.SetId(id);
                }
            }

            _changeTracker.Track(merchIssue);

            return merchIssue;
        }
        public async Task<MerchIssue> GetByEmployeeId(long employeeId, CancellationToken token)
        {
            const string sql = @"
                SELECT id, employee_id, merch_type_id, date_create, status_id
                FROM issues
                WHERE issues.employee_id = @EmployeeId;";

            var parameters = new
            {
                EmployeeId = employeeId
            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: token);
            var connection = await _dbConnectionFactory.CreateConnection(token);
            var result = await connection.QueryAsync<Models.MerchIssue>(commandDefinition);

            if (result.Count() == 0)
                return null;

            MerchIssue merchIssue = new MerchIssue(result.First().EmployeeId,
                result.Select(x => new MerchIssueItem(
                    x.Id,
                    new MerchType() {Value = MerchTypeEnum.From(x.MerchTypeId)},
                    x.DateCreate,
                    IssueStatusEnum.From(x.StatusId))).ToList());
            
            _changeTracker.Track(merchIssue);
            
            return merchIssue;
        }

        public async Task<List<MerchIssue>> GetAll(CancellationToken token)
        {
            const string sql = @"
                SELECT id, employee_id, merch_type_id, date_create, status_id
                FROM issues
                ORDER BY employee_id";

            var commandDefinition = new CommandDefinition(
                sql,
                commandTimeout: Timeout,
                cancellationToken: token);
            var connection = await _dbConnectionFactory.CreateConnection(token);
            var result = await connection.QueryAsync<Models.MerchIssue>(commandDefinition);

            if (result.Count() == 0)
                return new List<MerchIssue>();

            List<MerchIssue> merchIssues = new List<MerchIssue>();
            List<MerchIssueItem> issueItems = null;
            
            long employeeId = result.First().EmployeeId;
            MerchIssue merchIssue = null; 
            foreach (var issue in result)
            {
                if (employeeId != issue.EmployeeId)
                {
                    merchIssue = null;
                    employeeId = issue.EmployeeId;
                }
                
                if (merchIssue == null)
                {
                    merchIssue = new MerchIssue(employeeId);
                    issueItems = new List<MerchIssueItem>();
                    merchIssue.SetItems(issueItems);
                    merchIssues.Add(merchIssue);

                    _changeTracker.Track(merchIssue);
                }
                
                
                if (employeeId == issue.EmployeeId)
                {
                    issueItems.Add(new MerchIssueItem(
                        issue.Id,
                        new MerchType() {Value = MerchTypeEnum.From(issue.MerchTypeId)},
                        issue.DateCreate,
                        IssueStatusEnum.From(issue.StatusId)));
                }
            }

            return merchIssues;
        }
    }
}