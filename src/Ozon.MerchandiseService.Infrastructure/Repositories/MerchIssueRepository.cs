using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;
using Ozon.MerchandiseService.Domain.Seedwork;
using Ozon.MerchandiseService.Infrastructure.Repositories;

namespace Ozon.MerchandiseService.Infrastructure
{
    public class MerchIssueRepository: IMerchIssueRepository
    {
        private readonly IDbContext _context;

        public MerchIssueRepository(IDbContext context)
        {
            _context = context;
        }

        public void Add(MerchIssue merchIssue)
        {
            _context.MerchIssues.Add(merchIssue);
        }

        public MerchIssue GetById(int id)
        {
            return _context.MerchIssues.FirstOrDefault(x => x.Id == id);
        }

        public int Count()
        {
            return _context.MerchIssues.Count;
        }

        public List<MerchIssue> GetAll()
        {
            return _context.MerchIssues;
        }

        public void Save()
        {
            
        }
    }
}