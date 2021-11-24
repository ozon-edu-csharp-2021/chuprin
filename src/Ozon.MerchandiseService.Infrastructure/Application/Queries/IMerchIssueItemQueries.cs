using System.Collections.Generic;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;

namespace Ozon.MerchandiseService.Infrastructure.Application.Queries
{
    public interface IMerchIssueItemQueries
    {
        List<MerchIssue> GetMerchIssueItemWithStatus(IssueStatusEnum status, MerchType merchType,
            int quantityRecords = 1);
    }
}