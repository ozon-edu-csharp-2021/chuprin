using System;

namespace Ozon.MerchandiseService.Models.DbModels
{
    /// <summary>
    /// Информация о получении мерча
    /// </summary>
    public sealed class IssuanceMerch
    {
        public int Id { get; set; }
        public string MerchName { get; set;}
        public int Quantity { get; set;}
        public string FullnameEmployee { get; set;}
        
        public IssuanceMerch()
        {
        }
        public IssuanceMerch(int id, string merchName, int quantity, string fullnameEmployee)
        {
            Id = id;
            MerchName = merchName;
            Quantity = quantity;
            FullnameEmployee = fullnameEmployee;
        }
    }
}