using Ozon.MerchandiseService.Domain.SeedWork;

namespace Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate
{
    public class IssueStatusEnum: Enumeration
    {
        public static IssueStatusEnum Created = new IssueStatusEnum(1, nameof(Created)); //Создан
        public static IssueStatusEnum InQueue = new IssueStatusEnum(2, nameof(InQueue)); //В очереди на выдачу
        public static IssueStatusEnum PendingIssue = new IssueStatusEnum(3, nameof(PendingIssue)); //Ожидает выдачи
        public static IssueStatusEnum Issued = new IssueStatusEnum(4, nameof(Issued)); //Выдан

        public IssueStatusEnum(int id, string name) : base(id, name)
        {
        }
    }
}