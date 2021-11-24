using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ozon.MerchandiseService.Domain.AggregateModels.EmployeeAggregate;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;
using Ozon.MerchandiseService.Domain.Exceptions;
using Ozon.MerchandiseService.GrpcService.FakeServices;
using Ozon.MerchandiseService.GrpcService.FakeServices.EmailGrpc;

namespace Ozon.MerchandiseService.Infrastructure.Application.Commands
{
    public class IssueMerchCommandHandler: IRequestHandler<IssueMerchCommand, MerchIssue>
    {
        private readonly IMerchIssueRepository _merchIssueRepository;

        public IssueMerchCommandHandler(IMerchIssueRepository merchIssueRepository)
        {
            _merchIssueRepository = merchIssueRepository;
        }
        public async Task<MerchIssue> Handle(IssueMerchCommand request, CancellationToken cancellationToken)
        {
            var merchIssue = _merchIssueRepository.GetByEmployeeId(request.EmployeeId);

            if (merchIssue == null)
                throw new MerchandiseDomainException("Данного пользователя нет в системе");

            merchIssue.SetIssueStatus(new MerchType(){Value = MerchTypeEnum.From(request.MerchPackType)});
            
            await _merchIssueRepository.UnitOfWork.Save();

            return merchIssue;
        }
    }
}