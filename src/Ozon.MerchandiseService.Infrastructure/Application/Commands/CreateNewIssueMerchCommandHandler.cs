using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ozon.MerchandiseService.Domain.AggregateModels.EmployeeAggregate;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;
using Ozon.MerchandiseService.Domain.Exceptions;
using Ozon.MerchandiseService.Domain.SeedWork;
using Ozon.MerchandiseService.GrpcService.FakeServices;
using Ozon.MerchandiseService.GrpcService.FakeServices.EmailGrpc;

namespace Ozon.MerchandiseService.Infrastructure.Application.Commands
{
    public class CreateNewIssueMerchCommandHandler: IRequestHandler<CreateNewIssueMerchCommand, MerchIssue>
    {        
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMerchIssueRepository _merchIssueRepository;

        public CreateNewIssueMerchCommandHandler(IEmployeeRepository employeeRepository, IMerchIssueRepository merchIssueRepository)
        {
            _employeeRepository = employeeRepository;
            _merchIssueRepository = merchIssueRepository;
        }

        public async Task<MerchIssue> Handle(CreateNewIssueMerchCommand request, CancellationToken cancellationToken)
        {
            var employee = _employeeRepository.GetById(request.EmployeeId);
            if (employee == null)
            {
                employee = new Employee(request.EmployeeId);
                _employeeRepository.Add(employee);
            }
            await _employeeRepository.UnitOfWork.Save();

            var merchIssue = _merchIssueRepository.GetByEmployeeId(employee.EmployeeId);
            if (merchIssue == null)
            {
                merchIssue = new MerchIssue(employee.EmployeeId);
                _merchIssueRepository.Add(merchIssue);
            }
            merchIssue.AddMerchIssueItem(new MerchType() {Value = MerchTypeEnum.From(request.MerchType)}, request.DateRequest);
            await _merchIssueRepository.UnitOfWork.Save();

            return await Task.FromResult(merchIssue);
        }
    }
}