using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Ozon.MerchandiseService.HttpModels;

namespace Ozon.MerchandiseService.HttpClients
{
    public class MerchandiseServiceClient: IMerchandiseServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public MerchandiseServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IssuanceMerchInfoResponse> GetIssuanceMerchInfo(long id, CancellationToken token)
        {
            using var response = await _httpClient.GetAsync($"https://localhost:5001/api/GetInfoIssuanceMerch/{id}", token);
            var body = await response.Content.ReadAsStringAsync(token);
            return JsonSerializer.Deserialize<IssuanceMerchInfoResponse>(body, _jsonOptions);
        }

        public async Task<HttpResponseMessage> RequestMerch(RequestMerchPostViewModel requestMerchPostViewModel,
            CancellationToken token)
        {
            var json = JsonSerializer.Serialize(requestMerchPostViewModel);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            using var response = await _httpClient.PostAsync($"https://localhost:5001/api/RequestMerch",content, token);
            return response;
        }
    }
}