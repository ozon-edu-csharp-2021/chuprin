using System;
using System.Collections.Generic;
using System.Linq;
using Ozon.MerchandiseService.Domain.AggregateModels.EmployeeAggregate;
using Ozon.MerchandiseService.Domain.Events;
using Ozon.MerchandiseService.Domain.Exceptions;
using Ozon.MerchandiseService.Domain.Seedwork;
using Ozon.MerchandiseService.Domain.SeedWork;

namespace Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate
{
    public class MerchIssue : Entity, IAggregateRoot
    {
        
        public  long EmployeeId => _employeeId;
        private long _employeeId;

        public IReadOnlyCollection<MerchIssueItem> MerchIssueItems => _merchIssueItems;
        private List<MerchIssueItem> _merchIssueItems;
        
        public MerchIssue(long employeeId)
        {
            _employeeId = employeeId;
            _merchIssueItems = new List<MerchIssueItem>();
        }
        public MerchIssue(long employeeId, List<MerchIssueItem> items)
        {
            _employeeId = employeeId;
            _merchIssueItems = items;
        }
        
        public void AddMerchIssueItem(MerchType merchPackType, DateTime dateCreated)
        {
            var merchIssue = _merchIssueItems.FirstOrDefault(x => x.MerchPackType.Value.Id == merchPackType.Value.Id);
            
            if (merchIssue != null)
                throw new MerchandiseDomainException("Данный мерч уже был выдан сотруднику!");

            var newMerchIssueItem = new MerchIssueItem(merchPackType, dateCreated);
            _merchIssueItems.Add(newMerchIssueItem);
            AddMerchIssueItemCreatedDomainEvent(this, merchPackType);
        }

        public void SetInQueueStatus(MerchType merchType)
        {
            var merchIssueItem = GetMerchIssueItem(merchType);
            if(merchIssueItem == null)
                NotFoundMerchIssueItemWhenChangingStatusException(IssueStatusEnum.InQueue);
            
            if (merchIssueItem.IssueStatus.Id != IssueStatusEnum.IsCreated.Id)
                StatusChangeException(merchIssueItem.IssueStatus,IssueStatusEnum.InQueue);
            
            merchIssueItem.SetStatus(IssueStatusEnum.InQueue);
        }

        public void SetPendingStatus(MerchType merchType)
        {
            var merchIssueItem = GetMerchIssueItem(merchType);
            if (merchIssueItem == null)
                NotFoundMerchIssueItemWhenChangingStatusException(IssueStatusEnum.IsPending);

            if (merchIssueItem.IssueStatus.Id != IssueStatusEnum.InQueue.Id && merchIssueItem.IssueStatus.Id != IssueStatusEnum.IsCreated.Id)
                StatusChangeException(merchIssueItem.IssueStatus, IssueStatusEnum.IsPending);

            merchIssueItem.SetStatus(IssueStatusEnum.IsPending);
            AddMerchIssueItemChangedToPendingDomainEvent(merchType);
        }

        public void SetIssueStatus(MerchType merchType)
        {
            var merchIssueItem = GetMerchIssueItem(merchType);
            if(merchIssueItem == null)
                NotFoundMerchIssueItemWhenChangingStatusException(IssueStatusEnum.IsIssued);
            
            if (merchIssueItem.IssueStatus.Id != IssueStatusEnum.IsPending.Id)
                StatusChangeException(merchIssueItem.IssueStatus,IssueStatusEnum.IsIssued);

            merchIssueItem.SetStatus(IssueStatusEnum.IsIssued);
        }
        
        private MerchIssueItem GetMerchIssueItem(MerchType merchType)
        {
            var merchIssueItem = _merchIssueItems.FirstOrDefault(x => x.MerchPackType.Value.Id == merchType.Value.Id);

            return merchIssueItem;
        }

        public void SetItems(List<MerchIssueItem> items)
        {
            _merchIssueItems = items;
        }
        #region DomainEvents

        private void AddMerchIssueItemChangedToPendingDomainEvent(MerchType merchType)
        {
            var merchIssueStatusChangedToPendindDomainEvent = new MerchIssueStatusChangedToPendindDomainEvent(this, merchType);
            this.AddDomainEvent(merchIssueStatusChangedToPendindDomainEvent);
        }
        private void AddMerchIssueItemCreatedDomainEvent(MerchIssue merchIssue, MerchType merchType)
        {
            var merchIssueItemCreated = new MerchIssueItemCreatedDomainEvent(merchIssue, merchType);

            this.AddDomainEvent(merchIssueItemCreated);
        }

        #endregion

        #region Exceptions

        private void NotFoundMerchIssueItemWhenChangingStatusException(IssueStatusEnum status)
        {
            throw new MerchandiseDomainException($"Не удалось установить статус {status.Name}. Данный MerchIssueItem не найден.");
        }
        private void StatusChangeException(IssueStatusEnum oldStatus, IssueStatusEnum newStatus)
        {
            throw new MerchandiseDomainException($"Невозможно изменить статус с {oldStatus.Name} на {newStatus.Name}.");
        }

        #endregion
    }
}