using MediatR;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;

namespace Ozon.MerchandiseService.Infrastructure.Commands
{
    public class IssueMerchCommand: IRequest<MerchIssue>
    {
        /// <summary>
        /// Id заявки для выдачи
        /// </summary>
        public int MerchIssueId { get; set; }
    }
}