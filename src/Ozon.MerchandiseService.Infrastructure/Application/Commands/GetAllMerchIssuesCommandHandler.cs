using MediatR;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ozon.MerchandiseService.Domain.Seedwork;

namespace Ozon.MerchandiseService.Infrastructure.Application.Commands
{
    class GetAllMerchIssuesCommandHandler : IRequestHandler<GetAllMerchIssuesCommand, IReadOnlyCollection<MerchIssue>>
    {
        private readonly IMerchIssueRepository _merchIssueRepository;

        public GetAllMerchIssuesCommandHandler(IMerchIssueRepository merchIssueRepository)
        {
            _merchIssueRepository = merchIssueRepository;
        }

        public async Task<IReadOnlyCollection<MerchIssue>> Handle(GetAllMerchIssuesCommand request, CancellationToken cancellationToken)
        {
            return await _merchIssueRepository.GetAll(cancellationToken);
        }
    }
}
