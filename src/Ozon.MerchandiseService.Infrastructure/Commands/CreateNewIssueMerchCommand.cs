using MediatR;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;

namespace Ozon.MerchandiseService.Infrastructure.Commands
{
    /// <summary>
    /// Создать новую заявку на выдачу мерча
    /// </summary>
    public class CreateNewIssueMerchCommand: IRequest<MerchIssue>
    {
        /// <summary>
        /// Id сотрудника, которому нужно выдать мерч
        /// </summary>
        public long EmployeeId { get; set; }
        /// <summary>
        /// Тип MerchPackа
        /// </summary>
        public int MerchType { get; set; }
    }
}