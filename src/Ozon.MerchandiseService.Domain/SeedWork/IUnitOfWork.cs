using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ozon.MerchandiseService.Domain.Seedwork
{
    public interface IUnitOfWork : IDisposable
    {
        ValueTask StartTransaction(CancellationToken token);
        
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
