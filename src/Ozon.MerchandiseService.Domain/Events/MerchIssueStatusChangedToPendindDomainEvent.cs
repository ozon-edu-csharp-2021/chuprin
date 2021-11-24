using MediatR;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;

namespace Ozon.MerchandiseService.Domain.Events
{
    public class MerchIssueStatusChangedToPendindDomainEvent: INotification
    {
        public MerchIssue MerchIssue { get; }
        public MerchType MerchType { get; }
        public MerchIssueStatusChangedToPendindDomainEvent(MerchIssue merchIssue, MerchType merchType)
        {
            MerchIssue = merchIssue;
            MerchType = merchType;
        }
    }
}