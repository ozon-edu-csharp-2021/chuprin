using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ozon.MerchandiseService.Models;
using Ozon.MerchandiseService.Models.CreationModels;
using Ozon.MerchandiseService.Models.DbModels;
using Ozon.MerchandiseService.Models.Factories;
using Ozon.MerchandiseService.Services.Interfaces;

namespace Ozon.MerchandiseService.Services
{
    internal class MerchandiseService: IMerchandiseService
    {

        private readonly List<IssuanceMerch> _issuanceMerchItems = new List<IssuanceMerch>
        {
            new IssuanceMerch(1, "Футболка Ozon.MerchandiseService",1,"Чуприн Лев Юрьевич"),
            new IssuanceMerch(2,"Кепка Ozon.StockApi",1,"Чуприн Лев Юрьевич"),
            new IssuanceMerch(3,"Штаны Ozon.SupplyApi",2,"Свиридов Глеб")
        };


        public Task<IssuanceMerch> AddIssuanceMerch(IssuanceMerchCreationModel issuanceCreationModel, CancellationToken token)
        {
            var issuenceMerch = IssuanceMerchFactory.Create(issuanceCreationModel);
            issuenceMerch.Id = _issuanceMerchItems.Count + 1;
            
            _issuanceMerchItems.Add(issuenceMerch);

            return Task.FromResult(issuenceMerch);
        }

        public Task<IssuanceMerch> GetInfoIssuanceMerchItem(int id, CancellationToken token)
        {
            return Task.FromResult(_issuanceMerchItems.FirstOrDefault(x => x.Id == id));
        }
    }
}