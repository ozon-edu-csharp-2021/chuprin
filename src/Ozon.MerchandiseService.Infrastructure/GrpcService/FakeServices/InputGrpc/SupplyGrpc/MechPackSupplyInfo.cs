namespace Ozon.MerchandiseService.GrpcService.FakeServices.SupplyGrpc
{
    /// <summary>
    /// Используется в FakeController для симуляции работы Supply Service (только исходящие от него вызовы)
    /// </summary>
    public class MechPackSupplyInfo
    {
        public int MerchPackType { get; set; }
        public int Quantity { get; set; }
    }
}