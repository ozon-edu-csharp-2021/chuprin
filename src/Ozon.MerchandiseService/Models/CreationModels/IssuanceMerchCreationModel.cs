namespace Ozon.MerchandiseService.Models.CreationModels
{
    public class IssuanceMerchCreationModel
    {

        public int Id { get; set; }
        public string MerchName { get; set; }
        public int Quantity { get; set; }
        public string FullnameEmployee { get; set; }
        
        public IssuanceMerchCreationModel()
        {
        }
        public IssuanceMerchCreationModel(int id, string merchName, int quantity, string fullnameEmployee)
        {
            Id = id;
            MerchName = merchName;
            Quantity = quantity;
            FullnameEmployee = fullnameEmployee;
        }
    }
}