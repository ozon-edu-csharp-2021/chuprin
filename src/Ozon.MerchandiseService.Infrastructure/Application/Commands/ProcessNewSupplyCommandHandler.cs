using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;
using Ozon.MerchandiseService.GrpcService.FakeServices;
using Ozon.MerchandiseService.Infrastructure.Application.Queries;

namespace Ozon.MerchandiseService.Infrastructure.Application.Commands
{
    public class ProcessNewSupplyCommandHandler: IRequestHandler<ProcessNewSupplyCommand, List<MerchIssue>>
    {
        private readonly IMerchIssueItemQueries _merchIssueItemQueries;

        public ProcessNewSupplyCommandHandler(IMerchIssueItemQueries merchIssueItemQueries)
        {
            _merchIssueItemQueries = merchIssueItemQueries;
        }

        public async Task<List<MerchIssue>> Handle(ProcessNewSupplyCommand request, CancellationToken cancellationToken)
        {
            StockGrpcFakeService stockGrpc = new StockGrpcFakeService();
            List<MerchIssue> pendingMerchIssues = new List<MerchIssue>();
            
            foreach (var merchPackSupply in request.MechPackSupplies)
            {
                var merchType = new MerchType() {Value = MerchTypeEnum.From(merchPackSupply.MerchPackType)};
                
                var merchIssuesInQueue = _merchIssueItemQueries.GetMerchIssueItemWithStatus(IssueStatusEnum.InQueue,
                    merchType,
                    merchPackSupply.Quantity);

                foreach (var merchIssue in merchIssuesInQueue)
                {
                    if (stockGrpc.IssueMerchRequest(new IssueMerchRequest()
                        {MerchPackType = merchPackSupply.MerchPackType}))
                    {
                        merchIssue.SetPendingStatus(merchType);
                        
                        
                        if(pendingMerchIssues.FirstOrDefault(x=>x.EmployeeId == merchIssue.EmployeeId) == null)
                            pendingMerchIssues.Add(merchIssue);
                    }
                }
            }

            return await Task.FromResult(pendingMerchIssues);
        }
    }
}