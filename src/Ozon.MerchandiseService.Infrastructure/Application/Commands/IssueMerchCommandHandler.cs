using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ozon.MerchandiseService.Domain.AggregateModels.EmployeeAggregate;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;
using Ozon.MerchandiseService.Domain.Exceptions;
using Ozon.MerchandiseService.Domain.Seedwork;
using Ozon.MerchandiseService.GrpcService.FakeServices;
using Ozon.MerchandiseService.GrpcService.FakeServices.EmailGrpc;

namespace Ozon.MerchandiseService.Infrastructure.Application.Commands
{
    public class IssueMerchCommandHandler: IRequestHandler<IssueMerchCommand, MerchIssue>
    {
        private readonly IMerchIssueRepository _merchIssueRepository;
        private readonly IUnitOfWork _unitOfWork;

        public IssueMerchCommandHandler(IMerchIssueRepository merchIssueRepository, IUnitOfWork unitOfWork)
        {
            _merchIssueRepository = merchIssueRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<MerchIssue> Handle(IssueMerchCommand request, CancellationToken cancellationToken)
        {
            var merchIssue = await _merchIssueRepository.GetByEmployeeId(request.EmployeeId, cancellationToken);

            if (merchIssue == null)
                throw new MerchandiseDomainException("Данного пользователя нет в системе");

            await _unitOfWork.StartTransaction(cancellationToken);
            
            merchIssue.SetIssueStatus(new MerchType(){Value = MerchTypeEnum.From(request.MerchPackType)});
            await _merchIssueRepository.Update(merchIssue, cancellationToken);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return merchIssue;
        }
    }
}