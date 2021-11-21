using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;

namespace Ozon.MerchandiseService.Infrastructure.Application.Queries
{
    public interface IMerchIssueItemQueries
    {
        Task<List<MerchIssue>> GetMerchIssueItemWithStatus(IssueStatusEnum status, MerchType merchType,
            CancellationToken token, int quantityRecords = 1);
    }
}