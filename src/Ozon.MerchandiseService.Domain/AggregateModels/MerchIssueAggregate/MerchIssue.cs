using Ozon.MerchandiseService.Domain.AggregateModels.EmployeeAggregate;
using Ozon.MerchandiseService.Domain.Events;
using Ozon.MerchandiseService.Domain.Seedwork;
using Ozon.MerchandiseService.Domain.SeedWork;

namespace Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate
{
    public class MerchIssue : Entity, IAggregateRoot
    {
        public Employee Employee { get; private set; }
        public MerchType MerchPackType { get; private set; }
        public IssueStatusEnum IssueStatus { get; private set; }

        public MerchIssue(int id, Employee employee, MerchType merchPackType, IssueStatusEnum issueStatus)
        {
            Id = id;
            Employee = employee;
            MerchPackType = merchPackType;
            IssueStatus = issueStatus;
        }
        
        public MerchIssue(int id, Employee employee, MerchType merchPackType)
        {
            Id = id;
            Employee = employee;
            MerchPackType = merchPackType;
            IssueStatus = IssueStatusEnum.Created;
        }

        public void SetInQueueStatus()
        {
            if (IssueStatus.Id == IssueStatusEnum.Created.Id)
            {
                IssueStatus = IssueStatusEnum.InQueue;
                AddDomainEvent(new MerchIssueStatusChangedToInQueueDomainEvent(Id));
            }
        }

        public void SetPendingStatus()
        {
            if (IssueStatus.Id == IssueStatusEnum.Created.Id)
            {
                IssueStatus = IssueStatusEnum.PendingIssue;
                AddDomainEvent(new MerchIssueStatusChangedToPendindDomainEvent(Id));
            }
        }

        public void SetIssueStatus()
        {
            if (IssueStatus.Id == IssueStatusEnum.PendingIssue.Id)
            {
                IssueStatus = IssueStatusEnum.Issued;
                AddDomainEvent(new MerchIssueStatusChangedToIssueDomainEvent(Id));
            }
        }
    }
}