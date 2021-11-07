using System;
using System.Collections.Generic;

namespace Ozon.MerchandiseService.GrpcService.FakeServices
{
    /// <summary>
    /// Используется для симуляции Stock Api (только входящие в него вызовы)
    /// </summary>
    public class StockGrpcFakeService
    {
        //Остаток каждого MerchPack на складе
        public static Dictionary<int, int> AvailabilittyMerchPacks = new Dictionary<int, int>()
        {
            {1, 0},
            {2, 1},
            {3, 2},
            {4, 3},
            {5, 4}
        };
        private Random _rnd = new Random();
        

        /// <summary>
        /// Запрашивает выдачу мерча, если удалось забронировать true
        /// </summary>
        public bool IssueMerchRequest(IssueMerchRequest request)
        {
            if (AvailabilittyMerchPacks[request.MerchPackType] > 0)
            {
                AvailabilittyMerchPacks[request.MerchPackType] -= 1;
                return true;
            }

            return false;
        }
    }

    public class IssueMerchRequest
    {
        public int MerchPackType { get; set; }
    }
}