using MediatR;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ozon.MerchandiseService.Infrastructure.Application.Commands
{
    public class GetIssueByIdCommandHandler : IRequestHandler<GetIssueByIdCommand, MerchIssue>
    {
        private readonly IMerchIssueRepository _merchIssueRepository;

        public GetIssueByIdCommandHandler(IMerchIssueRepository merchIssueRepository)
        {
            _merchIssueRepository = merchIssueRepository;
        }

        public async Task<MerchIssue> Handle(GetIssueByIdCommand request, CancellationToken cancellationToken)
        {
            var result = _merchIssueRepository.GetById(request.Id);

            return await Task.FromResult(result);
        }
    }
}
