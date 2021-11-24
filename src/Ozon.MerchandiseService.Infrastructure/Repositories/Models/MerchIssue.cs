using System;

namespace Ozon.MerchandiseService.Infrastructure.Repositories.Models
{
    public class MerchIssue
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public int MerchTypeId { get; set; }
        public DateTime DateCreate { get; set; }
        public int StatusId { get; set; }
    }
}