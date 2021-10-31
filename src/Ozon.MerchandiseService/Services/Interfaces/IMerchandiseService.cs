using System.Threading;
using System.Threading.Tasks;
using Ozon.MerchandiseService.Models;
using Ozon.MerchandiseService.Models.CreationModels;
using Ozon.MerchandiseService.Models.DbModels;

namespace Ozon.MerchandiseService.Services.Interfaces
{
    public interface IMerchandiseService
    {
        Task<IssuanceMerch> AddIssuanceMerch(IssuanceMerchCreationModel issuanceCreationModel, CancellationToken token);
        Task<IssuanceMerch> GetInfoIssuanceMerchItem(int id, CancellationToken token);
    }
}