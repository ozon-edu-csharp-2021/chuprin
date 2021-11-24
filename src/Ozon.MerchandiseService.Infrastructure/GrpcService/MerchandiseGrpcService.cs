using System.Threading.Tasks;
using Grpc.Core;
using MediatR;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;
using Ozon.MerchandiseService.Grpc;
using Ozon.MerchandiseService.GrpcService.FakeServices;

namespace Ozon.MerchandiseService.GrpcService
{
    public class MerchandiseGrpcService: MerchandiseGrpcApi.MerchandiseGrpcApiBase
    {
        private readonly IMediator _mediator;

        public MerchandiseGrpcService(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}