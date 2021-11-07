using System.Collections.Generic;
using Ozon.MerchandiseService.GrpcService.FakeServices.SupplyGrpc;

namespace Ozon.MerchandiseService.Controllers.FakeServices
{
    public class NewSupplyRequest
    {
        public List<MechPackSupplyInfo> MerchSupplies { get; set; }
    }
}