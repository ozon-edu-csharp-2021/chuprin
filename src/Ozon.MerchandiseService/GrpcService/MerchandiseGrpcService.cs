using System.Threading.Tasks;
using Grpc.Core;
using Ozon.MerchandiseService.Grpc;
using Ozon.MerchandiseService.Models.CreationModels;
using Ozon.MerchandiseService.Services.Interfaces;

namespace Ozon.MerchandiseService.GrpcService
{
    public class MerchandiseGrpcService: MerchandiseGrpcApi.MerchandiseGrpcApiBase
    {
        private readonly IMerchandiseService _merchService;

        public MerchandiseGrpcService(IMerchandiseService merchService)
        {
            _merchService = merchService;
        }

        public override async Task<BaseResponse> RequestMerch(RequestMerchRequest request, ServerCallContext context)
        {
            await _merchService.AddIssuanceMerch(new IssuanceMerchCreationModel()
            {
                FullnameEmployee = request.FullName,
                MerchName = request.MerchName,
                Quantity = request.Quantity
            }, context.CancellationToken);
            
            return new BaseResponse()
            {
                StatusCode = 200,
                Message = "Запрос на получение мерча успешно добавлен!"
            };
        }

        public override async Task<InfoIssuanceMerchResponse> GetInfoIssuanceMerch(Int32BaseRequest request, ServerCallContext context)
        {
            var merch = await _merchService.GetInfoIssuanceMerchItem(request.Id, context.CancellationToken);
            return new InfoIssuanceMerchResponse()
            {                
                Id = merch.Id,
                FullName = merch.FullnameEmployee,
                MerchName = merch.MerchName,
                Quantity = merch.Quantity
            };
        }
    }
}