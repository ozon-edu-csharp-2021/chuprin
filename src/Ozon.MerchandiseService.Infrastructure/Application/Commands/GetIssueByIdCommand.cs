using MediatR;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ozon.MerchandiseService.Infrastructure.Application.Commands
{
    public class GetIssueByIdCommand : IRequest<MerchIssue>
    {
        public int Id { get; set; }
    }
}
