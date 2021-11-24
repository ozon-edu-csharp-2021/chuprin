using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ozon.MerchandiseService.Domain.Seedwork;

namespace Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate
{
    public interface IMerchIssueRepository
    {
        void Add(MerchIssue merchIssue);
        MerchIssue GetById(int id);
        MerchIssue GetByEmployeeId(long employeeId);
        List<MerchIssue> GetAll();
        IDbContext UnitOfWork { get; }
    }
}