using System.Collections.Generic;
using System.Linq;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;

namespace Ozon.MerchandiseService.Infrastructure.Application.Queries
{
    public class MerchIssueItemQueries: IMerchIssueItemQueries
    {
        private readonly IMerchIssueRepository _merchIssueRepository;

        public MerchIssueItemQueries(IMerchIssueRepository merchIssueRepository)
        {
            _merchIssueRepository = merchIssueRepository;
        }

        public List<MerchIssue> GetMerchIssueItemWithStatus(IssueStatusEnum status, MerchType merchType, int quantityRecords = 1)
        {
            List<MerchIssue> result = new List<MerchIssue>();
            
            var merchIssues = _merchIssueRepository.GetAll();
            foreach (var merchIssue in merchIssues)
            {
                if (merchIssue.MerchIssueItems
                    .Where(x => x.IssueStatus == status && x.MerchPackType.Value.Id == merchType.Value.Id)
                    .OrderBy(x => x.DateCreated)
                    .Any())
                {
                    result.Add(merchIssue);
                    
                    if(quantityRecords == result.Count)
                        break;
                }
            }

            return result;
        }
    }
}