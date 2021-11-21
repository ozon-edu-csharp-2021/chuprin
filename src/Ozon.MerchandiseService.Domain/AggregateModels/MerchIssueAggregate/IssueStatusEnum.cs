using System.Collections.Generic;
using System.Linq;
using Ozon.MerchandiseService.Domain.Exceptions;
using Ozon.MerchandiseService.Domain.SeedWork;

namespace Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate
{
    public class IssueStatusEnum: Enumeration
    {
        public static IssueStatusEnum IsCreated = new IssueStatusEnum(1, nameof(IsCreated)); //Создан
        public static IssueStatusEnum InQueue = new IssueStatusEnum(2, nameof(InQueue)); //В очереди на выдачу
        public static IssueStatusEnum IsPending = new IssueStatusEnum(3, nameof(IsPending)); //Ожидает выдачи
        public static IssueStatusEnum IsIssued = new IssueStatusEnum(4, nameof(IsIssued)); //Выдан

        public IssueStatusEnum(int id, string name) : base(id, name)
        {
        }
        
        public static IEnumerable<IssueStatusEnum> List() =>
            new[] { IsCreated, InQueue, IsPending, IsIssued };
        public static IssueStatusEnum From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new MerchandiseDomainException($"Нет указанного статуса");
            }

            return state;
        }
    }
}