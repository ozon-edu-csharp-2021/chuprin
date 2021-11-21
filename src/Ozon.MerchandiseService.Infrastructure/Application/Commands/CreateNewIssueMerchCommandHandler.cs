using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ozon.MerchandiseService.Domain.AggregateModels.EmployeeAggregate;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;
using Ozon.MerchandiseService.Domain.Exceptions;
using Ozon.MerchandiseService.Domain.Seedwork;
using Ozon.MerchandiseService.Domain.SeedWork;
using Ozon.MerchandiseService.GrpcService.FakeServices;
using Ozon.MerchandiseService.GrpcService.FakeServices.EmailGrpc;

namespace Ozon.MerchandiseService.Infrastructure.Application.Commands
{
    public class CreateNewIssueMerchCommandHandler: IRequestHandler<CreateNewIssueMerchCommand, MerchIssue>
    {        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMerchIssueRepository _merchIssueRepository;

        public CreateNewIssueMerchCommandHandler(IEmployeeRepository employeeRepository, IMerchIssueRepository merchIssueRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _merchIssueRepository = merchIssueRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<MerchIssue> Handle(CreateNewIssueMerchCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.StartTransaction(cancellationToken);
            var employee = await _employeeRepository.FindByEmployeeId(request.EmployeeId, cancellationToken);
            if (employee == null)
            {
                employee = new Employee(request.EmployeeId);
                await _employeeRepository.Add(employee, cancellationToken);
            }

            var merchIssue = await _merchIssueRepository.GetByEmployeeId(employee.EmployeeId, cancellationToken);
            if (merchIssue == null)
            {
                merchIssue = new MerchIssue(employee.EmployeeId);
            }

            merchIssue.AddMerchIssueItem(new MerchType() {Value = MerchTypeEnum.From(request.MerchType)},
                request.DateRequest);
            await _merchIssueRepository.Add(merchIssue, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(merchIssue);
        }
    }
}