using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Ozon.MerchandiseService.HttpModels;

namespace Ozon.MerchandiseService.HttpClients
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HttpClient client = new HttpClient();
            CancellationToken token = new CancellationToken();
            
            IMerchandiseServiceClient merchClient = new MerchandiseServiceClient(client);
            
            PrintIssuance(await merchClient.GetIssuanceMerchInfo(1,token));

            var requestMerchPostVm = new RequestMerchPostViewModel()
            {
                FullName = "Петров Петр Петрович",
                MerchName = "Наушники Ozon.Pro",
                Quantity = 2
            };
            PrintResultRequest(await merchClient.RequestMerch(requestMerchPostVm, token));
            
            PrintIssuance(await merchClient.GetIssuanceMerchInfo(4,token));
            
            Console.ReadKey();
        }

        private static void PrintResultRequest(HttpResponseMessage responseMessage)
        {
            Console.WriteLine($"{responseMessage.StatusCode}");
        }
        private static void PrintIssuance(IssuanceMerchInfoResponse issuanceMerchResponse)
        {
            Console.WriteLine($"{issuanceMerchResponse.FullName} - {issuanceMerchResponse.MerchName} - {issuanceMerchResponse.Quantity}шт.");
        }
    }
}