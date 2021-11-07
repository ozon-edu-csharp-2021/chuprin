using System;
using MediatR;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;

namespace Ozon.MerchandiseService.Infrastructure.Application.Commands
{
    /// <summary>
    /// Создать новую заявку на выдачу мерча
    /// </summary>
    public class CreateNewIssueMerchCommand: IRequest<MerchIssue>
    {
        /// <summary>
        /// Id сотрудника, которому нужно выдать мерч
        /// </summary>
        public long EmployeeId { get;}
        /// <summary>
        /// Тип MerchPackа
        /// </summary>
        public int MerchType { get;  }
        /// <summary>
        /// Дата заявки на выдачу мерча
        /// </summary>
        public DateTime DateRequest { get; }
        public CreateNewIssueMerchCommand(long employeeId, int merchType, DateTime dateRequest)
        {
            EmployeeId = employeeId;
            MerchType = merchType;
            DateRequest = dateRequest;
        }
    }
}