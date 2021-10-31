using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Ozon.MerchandiseService.HttpModels;

namespace Ozon.MerchandiseService.HttpClients
{
    public interface IMerchandiseServiceClient
    {
        Task<IssuanceMerchInfoResponse> GetIssuanceMerchInfo(long id, CancellationToken token);
        Task<HttpResponseMessage> RequestMerch(RequestMerchPostViewModel requestMerchPostViewModel,
            CancellationToken token);
    }
}