using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;
using Ozon.MerchandiseService.Domain.Events;
using Ozon.MerchandiseService.GrpcService.FakeServices.EmailGrpc;

namespace Ozon.MerchandiseService.Infrastructure.Application.DomainEventHandlers
{
    public class MerchIssueStatusChangedToPendindDomainEventHandler: INotificationHandler<MerchIssueStatusChangedToPendindDomainEvent>
    {
        private readonly IMerchIssueRepository _merchIssueRepository;
        public MerchIssueStatusChangedToPendindDomainEventHandler()
        {
        }

        public async Task Handle(MerchIssueStatusChangedToPendindDomainEvent notification, CancellationToken cancellationToken)
        {
            EmailFakeGrpcService emailService = new EmailFakeGrpcService();
            await Task.Run(() => emailService.SendEmail(new SendEmailRequest()
            {
                EmployeeId = notification.MerchIssue.EmployeeId,
                MerchPackType = notification.MerchType.Value.Id
            }));
        }
    }
}