using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;
using Ozon.MerchandiseService.GrpcService.FakeServices;
using Ozon.MerchandiseService.HttpModels;
using Ozon.MerchandiseService.Infrastructure.Application.Commands;
using Ozon.MerchandiseService.Models.Factories;

namespace Ozon.MerchandiseService.Controllers.FakeServices
{
    [ApiController]
    [Route("/FakeService/")]
    [Produces("application/json")]
    public class FakeOtherServicesController: Controller
    {
        private readonly IMediator _mediator;

        public FakeOtherServicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Request FROM Supply Service. Пришла новая поставка на склад
        /// </summary>
        [HttpPost]
        [Route("/SupplyService/NewSupply")]
        public async Task<IReadOnlyCollection<MerchIssueDto>> NewSupply([FromBody]NewSupplyRequest request, CancellationToken token)
        {
            //Обновим количество на складе (работа самого StockApi)
            foreach (var merchSupply in request.MerchSupplies)
            {
                StockGrpcFakeService.AvailabilittyMerchPacks[merchSupply.MerchPackType] += merchSupply.Quantity;
            }
            
            return MerchIssueDtoFactory.Create(await _mediator.Send(new ProcessNewSupplyCommand()
            {
                MechPackSupplies = request.MerchSupplies
            }));
        }
        /// <summary>
        /// Request FROM Employee Service. Добавить новую заявку на мерч
        /// </summary>
        [HttpPost]
        [Route("/EmployeeService/CreateNewIssueMerch")]
        public async Task<MerchIssueDto> CreateNewIssueMerch([FromBody]NewMerchRequest request, CancellationToken token)
        {
            var command = new CreateNewIssueMerchCommand(
                request.EmployeeId,
                request.MerchPackType,
                request.DateRequest
            );
            
            return MerchIssueDtoFactory.Create(await _mediator.Send(command));
        }
    }
}