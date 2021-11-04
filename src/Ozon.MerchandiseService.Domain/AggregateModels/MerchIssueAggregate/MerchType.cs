using System.Collections.Generic;
using Ozon.MerchandiseService.Domain.SeedWork;

namespace Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate
{
    public class MerchType: ValueObject
    {
        public MerchTypeEnum Value { get; set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}