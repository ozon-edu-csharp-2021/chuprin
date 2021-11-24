using System;
using System.Linq;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;
using Ozon.MerchandiseService.Domain.Exceptions;
using Xunit;

namespace Ozon.MerchandiseService.Domain.Tests
{
    public class MerchIssueAggregateTest
    {
        [Fact]
        public void Check_created_status_when_add_new_merch_issue_item()
        {
            //Arrange    
            var merchIssue = new MerchIssue(10);
            //Act 
            merchIssue.AddMerchIssueItem(new MerchType(){Value = MerchTypeEnum.From(1)} , DateTime.Now);
            //Assert
            Assert.Equal(IssueStatusEnum.IsCreated, merchIssue.MerchIssueItems.First().IssueStatus);
        }
        [Fact]
        public void Invalid_status_when_change_to_pending_1()
        {
            //Arrange    
            var merchIssue = new MerchIssue(10);
            var merchType = new MerchType() {Value = MerchTypeEnum.From(1)};
            //Act 
            merchIssue.AddMerchIssueItem(merchType, DateTime.Now);
            merchIssue.SetPendingStatus(merchType);
            merchIssue.SetIssueStatus(merchType);
            //Assert
            Assert.Throws<MerchandiseDomainException>(() => merchIssue.SetPendingStatus(merchType));
            Assert.Throws<MerchandiseDomainException>(() => merchIssue.SetInQueueStatus(merchType));
        }
        [Fact]
        public void Invalid_status_when_change_to_in_queue_1()
        {
            //Arrange    
            var merchIssue = new MerchIssue(10);
            var merchType = new MerchType() {Value = MerchTypeEnum.From(1)};
            //Act 
            merchIssue.AddMerchIssueItem(merchType, DateTime.Now);
            merchIssue.SetInQueueStatus(merchType);
            //Assert
            Assert.Throws<MerchandiseDomainException>(() => merchIssue.SetInQueueStatus(merchType));
            Assert.Throws<MerchandiseDomainException>(() => merchIssue.SetIssueStatus(merchType));
        }
        [Fact]
        public void Invalid_status_when_change_to_in_queue_2()
        {
            //Arrange    
            var merchIssue = new MerchIssue(10);
            var merchType = new MerchType() {Value = MerchTypeEnum.From(1)};
            //Act 
            merchIssue.AddMerchIssueItem(merchType, DateTime.Now);
            merchIssue.SetPendingStatus(merchType);
            //Assert
            Assert.Throws<MerchandiseDomainException>(() => merchIssue.SetInQueueStatus(merchType));
        }
        [Fact]
        public void Invalid_status_when_change_to_issued_1()
        {
            //Arrange    
            var merchIssue = new MerchIssue(10);
            var merchType = new MerchType() {Value = MerchTypeEnum.From(1)};
            //Act 
            merchIssue.AddMerchIssueItem(merchType, DateTime.Now);
            //Assert
            Assert.Throws<MerchandiseDomainException>(() => merchIssue.SetIssueStatus(merchType));
        }
        [Fact]
        public void Invalid_status_when_change_to_issued_2()
        {
            //Arrange    
            var merchIssue = new MerchIssue(10);
            var merchType = new MerchType() {Value = MerchTypeEnum.From(1)};
            //Act 
            merchIssue.AddMerchIssueItem(merchType, DateTime.Now);
            merchIssue.SetInQueueStatus(merchType);
            //Assert
            Assert.Throws<MerchandiseDomainException>(() => merchIssue.SetIssueStatus(merchType));
        }
    }
}