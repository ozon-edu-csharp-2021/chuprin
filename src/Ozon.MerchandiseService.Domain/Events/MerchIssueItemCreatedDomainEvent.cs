using MediatR;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;

namespace Ozon.MerchandiseService.Domain.Events
{
    public class MerchIssueItemCreatedDomainEvent: INotification    
    {
        public MerchIssue MerchIssue { get; }
        public MerchType MerchType { get; }
        public MerchIssueItemCreatedDomainEvent(MerchIssue merchIssue, MerchType merchType)
        {
            MerchIssue = merchIssue;
            MerchType = merchType;
        }
    }
}