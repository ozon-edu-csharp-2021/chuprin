using System;

namespace Ozon.MerchandiseService.Controllers.FakeServices
{
    public class NewMerchRequest
    {
        public long EmployeeId { get; set; }
        public int MerchPackType { get; set; }
        public DateTime DateRequest { get; set; }
    }
}