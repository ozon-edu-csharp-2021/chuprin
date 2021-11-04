using System;
using System.Collections.Generic;
using Ozon.MerchandiseService.Infrastructure.Commands;

namespace Ozon.MerchandiseService.GrpcService.FakeServices
{
    public class StockGrpcFakeService
    {
        //Остаток каждого MerchPack на складе
        private Dictionary<int, int> _availabilittyMerchPacks = new Dictionary<int, int>()
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
            return _availabilittyMerchPacks[request.MerchPackType] > 0;
        }
    }

    public class IssueMerchRequest
    {
        public int MerchPackType { get; set; }
    }
}