using MediatR;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;

namespace Ozon.MerchandiseService.Domain.Events
{
    public class MerchIssueStatusChangedToIssueDomainEvent: INotification
    {
        public MerchIssueStatusChangedToIssueDomainEvent(int merchIssueId)
        {
            MerchIssueId = merchIssueId;
        }

        public int MerchIssueId { get; set; }
        
    }
}