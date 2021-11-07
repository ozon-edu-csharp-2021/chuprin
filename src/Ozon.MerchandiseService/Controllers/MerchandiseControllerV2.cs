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
        private readonly IMerchIssueRepository _merchIssueRepository;
       
        public MerchandiseControllerV2(IMediator mediator, IMerchIssueRepository merchIssueRepository)
        {
            _mediator = mediator;
            _merchIssueRepository = merchIssueRepository;
        }

        /// <summary>
        /// Получить информацию о выдаче
        /// </summary>
        [Route("GetMerchIssueInfo/{id:int}")]
        [HttpGet]
        public async Task<MerchIssueDto> GetMerchIssue(int id, CancellationToken token)
        {
            var merchIssue = _merchIssueRepository.GetById(id);
            
            return await Task.FromResult(MerchIssueDtoFactory.Create(merchIssue));
        }
        
        /// <summary>
        /// Выдать мерч по заявке
        /// </summary>
        [Route("RequestMerch")]
        [HttpPost]
        public async Task<MerchIssueDto> RequestMerch([FromBody]IssueMerchCommand requestMerchVM, CancellationToken token)
        {
            var result = await _mediator.Send(requestMerchVM);

            return MerchIssueDtoFactory.Create(result);
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<List<MerchIssueDto>> GetAll(CancellationToken token)
        {
            var dtos = MerchIssueDtoFactory.Create(_merchIssueRepository.GetAll());
            return await Task.FromResult(dtos);
        }
    }
}