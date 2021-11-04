using MediatR;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;

namespace Ozon.MerchandiseService.Domain.Events
{
    public class MerchIssueStatusChangedToPendindDomainEvent: INotification
    {
        public MerchIssueStatusChangedToPendindDomainEvent(int merchIssueId)
        {
            MerchIssueId = merchIssueId;
        }

        public int MerchIssueId { get; set; }
        
    }
}