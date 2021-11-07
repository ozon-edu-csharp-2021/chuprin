using System.Collections.Generic;
using MediatR;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;
using Ozon.MerchandiseService.GrpcService.FakeServices.SupplyGrpc;

namespace Ozon.MerchandiseService.Infrastructure.Application.Commands
{
    /// <summary>
    /// Обработка поступления мерча на склад
    /// </summary>
    public class ProcessNewSupplyCommand: IRequest<List<MerchIssue>>
    {
        public List<MechPackSupplyInfo> MechPackSupplies;
    }
}