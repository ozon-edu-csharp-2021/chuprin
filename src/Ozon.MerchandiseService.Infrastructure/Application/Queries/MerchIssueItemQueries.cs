using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

        public async Task<List<MerchIssue>> GetMerchIssueItemWithStatus(IssueStatusEnum status, MerchType merchType,CancellationToken token, int quantityRecords = 1)
        {
            List<MerchIssue> result = new List<MerchIssue>();
            
            var merchIssues = await _merchIssueRepository.GetAll(token);
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