using Ozon.MerchandiseService.Models.CreationModels;
using Ozon.MerchandiseService.Models.DbModels;

namespace Ozon.MerchandiseService.Models.Factories
{
    internal static class IssuanceMerchFactory
    {
        public static IssuanceMerch Create(IssuanceMerchCreationModel creationModel)
        {
            IssuanceMerch dbModel = new IssuanceMerch()
            {
                Id = creationModel.Id,
                MerchName = creationModel.MerchName,
                Quantity = creationModel.Quantity,
                FullnameEmployee = creationModel.FullnameEmployee
            };
            
            return dbModel;
        }
    }
}