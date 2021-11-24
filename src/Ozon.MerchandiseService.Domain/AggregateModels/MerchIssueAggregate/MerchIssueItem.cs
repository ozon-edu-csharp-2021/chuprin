using System;
using Ozon.MerchandiseService.Domain.Events;
using Ozon.MerchandiseService.Domain.Seedwork;

namespace Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate
{
    public class MerchIssueItem: Entity
    {
        public DateTime DateCreated { get;private set; }
        public MerchType MerchPackType { get;private set;  }
        public IssueStatusEnum IssueStatus { get; private set; }
        
        public MerchIssueItem(MerchType merchPackType,DateTime dateCreated)
        {
            DateCreated = dateCreated;
            MerchPackType = merchPackType;
            IssueStatus = IssueStatusEnum.IsCreated;
        }

        public void SetStatus(IssueStatusEnum status)
        {
            IssueStatus = status;
        }
    }
}