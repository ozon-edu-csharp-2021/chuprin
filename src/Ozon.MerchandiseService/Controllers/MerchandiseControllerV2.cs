using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ozon.MerchandiseService.Domain.AggregateModels.EmployeeAggregate;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;
using Ozon.MerchandiseService.Grpc;
using Ozon.MerchandiseService.HttpModels;
using Ozon.MerchandiseService.Infrastructure.Application.Commands;
using Ozon.MerchandiseService.Models.CreationModels;
using Ozon.MerchandiseService.Models.Factories;
using Ozon.MerchandiseService.Services.Interfaces;

namespace Ozon.MerchandiseService.Controllers
{
    [Route("/api/v2")]
    [Produces("application/json")]    
    [ApiController]
    public class MerchandiseControllerV2: ControllerBase
    {
        private readonly IMediator _mediator;
       
        public MerchandiseControllerV2(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Получить информацию о выдаче
        /// </summary>
        [Route("GetMerchIssueInfo/{id:int}")]
        [HttpGet]
        public async Task<MerchIssueDto> GetMerchIssue([FromBody]GetIssueByIdCommand getEmployeeRequest, CancellationToken token)
        {
            var merchIssue = await _mediator.Send(getEmployeeRequest, token);
            
            return await Task.FromResult(MerchIssueDtoFactory.Create(merchIssue));
        }
        
        /// <summary>
        /// Выдать мерч по заявке
        /// </summary>
        [Route("RequestMerch")]
        [HttpPost]
        public async Task<MerchIssueDto> RequestMerch([FromBody]IssueMerchCommand requestMerchIssue, CancellationToken token)
        {
            var result = await _mediator.Send(requestMerchIssue, token);

            return MerchIssueDtoFactory.Create(result);
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<IReadOnlyCollection<MerchIssueDto>> GetAll(CancellationToken token)
        {
            var merchIssues = await _mediator.Send(new GetAllMerchIssuesCommand(), token);

            var dtos = MerchIssueDtoFactory.Create(merchIssues);

            return await Task.FromResult(dtos);
        }
    }
}