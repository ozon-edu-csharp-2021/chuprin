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
using Ozon.MerchandiseService.Infrastructure.Commands;
using Ozon.MerchandiseService.Models.CreationModels;
using Ozon.MerchandiseService.Services.Interfaces;

namespace Ozon.MerchandiseService.Controllers
{
    [ApiController]
    [Route("/api/v2")]
    [Produces("application/json")]
    public class MerchandiseControllerV2: ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMerchIssueRepository _merchIssueRepository;
       
        public MerchandiseControllerV2(IMediator mediator, IMerchIssueRepository merchIssueRepository)
        {
            _mediator = mediator;
            _merchIssueRepository = merchIssueRepository;
        }

        /// <summary>
        /// Получить информацию о выдаче
        /// </summary>
        [HttpGet]
        [Route("GetMerchIssueInfo/{id:int}")]
        public async Task<MerchIssue> GetMerchIssue(int id, CancellationToken token)
        {
            return await Task.FromResult(_merchIssueRepository.GetById(id));
        }
        
        /// <summary>
        /// Выдать мерч по заявке
        /// </summary>
        [HttpPost]
        [Route("RequestMerch")]
        public async Task<MerchIssue> RequestMerch([FromBody]IssueMerchCommand requestMerchVM, CancellationToken token)
        {
            var result = await _mediator.Send(requestMerchVM);

            return result;
        }

        /// <summary>
        /// Добавить новую заявку на мерч из Employee Service
        /// </summary>
        [HttpPost]
        [Route("CreateNewIssueMerch")]
        public async Task<MerchIssue> CreateNewIssueMerch([FromBody]CreateNewIssueMerchRequest request, CancellationToken token)
        {
            var command = new CreateNewIssueMerchCommand()
            {
                EmployeeId = request.EmployeeId,
                MerchType = request.MerchPack
            };
            
            MerchIssue merchIssue = await _mediator.Send(command);

            return merchIssue;
        }
    }
}