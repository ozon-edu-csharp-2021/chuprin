using System.Collections.Generic;
using System.Linq;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;
using Ozon.MerchandiseService.HttpModels;

namespace Ozon.MerchandiseService.Models.Factories
{
    public class MerchIssueDtoFactory
    {
        public static List<MerchIssueDto> Create(List<MerchIssue> merchIssues)
        {
            if (merchIssues == null)
                return null;

            return merchIssues.Select(Create).ToList();
        }
        public static MerchIssueDto Create(MerchIssue merchIssue)
        {
            if (merchIssue == null)
                return null;
            
            var merchIssueDto = new MerchIssueDto(merchIssue.EmployeeId);
            foreach (var item in merchIssue.MerchIssueItems)
            {
                merchIssueDto.AddMerchIssueItemDto(CreateItem(item));
            }

            return merchIssueDto;
        }
        public static List<MerchIssueItemDto> CreateItem(List<MerchIssueItem> merchIssues)
        {
            if (merchIssues == null)
                return null;

            return merchIssues.Select(CreateItem).ToList();
        }
        public static MerchIssueItemDto CreateItem(MerchIssueItem merchIssueItem)
        {
            if (merchIssueItem == null)
                return null;

            return new MerchIssueItemDto(merchIssueItem.MerchPackType.Value.Name, merchIssueItem.IssueStatus.Name);
        }
    }
}