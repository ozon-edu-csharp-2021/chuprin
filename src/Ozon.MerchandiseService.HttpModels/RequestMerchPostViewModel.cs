using System;

namespace Ozon.MerchandiseService.HttpModels
{
    public class RequestMerchPostViewModel
    {
        public string MerchName { get; set; }
        public string FullName { get; set; }
        public int Quantity { get; set; }
    }
}