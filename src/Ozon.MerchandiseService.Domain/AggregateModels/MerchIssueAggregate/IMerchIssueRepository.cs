using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ozon.MerchandiseService.Domain.Seedwork;

namespace Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate
{
    public interface IMerchIssueRepository
    {
        Task<MerchIssue> Add(MerchIssue merchIssue, CancellationToken token);
        Task<MerchIssue> GetById(int id, CancellationToken token);
        Task<MerchIssue> GetByEmployeeId(long employeeId, CancellationToken token);
        Task<List<MerchIssue>> GetAll(CancellationToken token);
        Task<MerchIssue> Update(MerchIssue merchIssue, CancellationToken token);
    }
}