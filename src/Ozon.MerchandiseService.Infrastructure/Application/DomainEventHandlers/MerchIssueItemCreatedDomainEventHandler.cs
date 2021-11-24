using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;
using Ozon.MerchandiseService.Domain.Events;
using Ozon.MerchandiseService.Domain.Seedwork;
using Ozon.MerchandiseService.GrpcService.FakeServices;

namespace Ozon.MerchandiseService.Infrastructure.Application.DomainEventHandlers
{
    public class MerchIssueItemCreatedDomainEventHandler: INotificationHandler<MerchIssueItemCreatedDomainEvent>
    {
        private readonly IMerchIssueRepository _merchIssueRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MerchIssueItemCreatedDomainEventHandler(IMerchIssueRepository merchIssueRepository, IUnitOfWork unitOfWork)
        {
            _merchIssueRepository = merchIssueRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(MerchIssueItemCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            StockGrpcFakeService stockApi = new StockGrpcFakeService();

            var issueRequest = new IssueMerchRequest() {MerchPackType = notification.MerchType.Value.Id};

            if (stockApi.IssueMerchRequest(issueRequest))
                notification.MerchIssue.SetPendingStatus(notification.MerchType); //Ожидает выдачи сотруднику
            else
                notification.MerchIssue.SetInQueueStatus(notification.MerchType);

            await _merchIssueRepository.Update(notification.MerchIssue, cancellationToken);
        }
    }
}