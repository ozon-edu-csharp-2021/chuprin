using MediatR;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;

namespace Ozon.MerchandiseService.Infrastructure.Application.Commands
{
    public class IssueMerchCommand: IRequest<MerchIssue>
    {
        /// <summary>
        /// Id сотрудника
        /// </summary>
        public long EmployeeId { get; set; }
        /// <summary>
        /// Id MerchPack-a
        /// </summary>
        public int MerchPackType { get; set; }
    }
}