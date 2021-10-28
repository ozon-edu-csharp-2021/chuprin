using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ozon.MerchandiseService.HttpModels;
using Ozon.MerchandiseService.Models;
using Ozon.MerchandiseService.Models.CreationModels;
using Ozon.MerchandiseService.Models.DbModels;
using Ozon.MerchandiseService.Services.Interfaces;

namespace Ozon.MerchandiseService.Controllers
{
    [ApiController]
    [Route("/api")]
    [Produces("application/json")]
    public class MerchandiseController: ControllerBase
    {
        private readonly IMerchandiseService _merchandiseService;

        public MerchandiseController(IMerchandiseService merchandiseService)
        {
            _merchandiseService = merchandiseService;
        }

        /// <summary>
        /// Запросить мерч
        /// </summary>
        [HttpPost]
        [Route("RequestMerch")]
        public async Task<IActionResult> RequestMerch([FromBody]RequestMerchPostViewModel requestMerchVM, CancellationToken token)
        {
            var issuanceMerch = await _merchandiseService.AddIssuanceMerch(new IssuanceMerchCreationModel()
            {
                Id = 0,
                FullnameEmployee = requestMerchVM.FullName,
                MerchName = requestMerchVM.MerchName,
                Quantity = requestMerchVM.Quantity
            }, token);

            return Ok();
        }
        
        /// <summary>
        /// Получить информацию о выдаче мерча
        /// </summary>
        [HttpGet]
        [Route("GetInfoIssuanceMerch/{id:int}")]
        public async Task<ActionResult<IssuanceMerchInfoResponse>> GetInfoIssuanceMerch(int id,
            CancellationToken token)
        {
            IssuanceMerchInfoResponse response = null;
            var infoIssuence = await _merchandiseService.GetInfoIssuanceMerchItem(id, token);
            if (infoIssuence == null)
                return NotFound();
            
            response = new IssuanceMerchInfoResponse()
            {
                FullName = infoIssuence.FullnameEmployee,
                MerchName = infoIssuence.MerchName,
                Quantity = infoIssuence.Quantity
            };
            
            return response;
        }
    }
}