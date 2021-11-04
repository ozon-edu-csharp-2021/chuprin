using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ozon.MerchandiseService.Domain.AggregateModels.EmployeeAggregate;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;
using Ozon.MerchandiseService.GrpcService.FakeServices;
using Ozon.MerchandiseService.GrpcService.FakeServices.EmailGrpc;
using Ozon.MerchandiseService.Infrastructure.Commands;

namespace Ozon.MerchandiseService.Infrastructure.Handlers
{
    public class IssueMerchHandler: IRequestHandler<IssueMerchCommand, MerchIssue>
    {
        private readonly IMerchIssueRepository _merchIssueRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public IssueMerchHandler(IMerchIssueRepository merchIssueRepository, IEmployeeRepository employeeRepository)
        {
            _merchIssueRepository = merchIssueRepository;
            _employeeRepository = employeeRepository;
        }
        public async Task<MerchIssue> Handle(IssueMerchCommand request, CancellationToken cancellationToken)
        {
            var merchIssue = _merchIssueRepository.GetById(request.MerchIssueId);
            
            merchIssue.SetIssueStatus();
            
            _merchIssueRepository.Save();

            return await Task.FromResult(merchIssue);
        }
    }
}