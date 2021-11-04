using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ozon.MerchandiseService.Domain.AggregateModels.EmployeeAggregate;
using Ozon.MerchandiseService.Domain.AggregateModels.MerchIssueAggregate;
using Ozon.MerchandiseService.Domain.SeedWork;
using Ozon.MerchandiseService.GrpcService.FakeServices;
using Ozon.MerchandiseService.GrpcService.FakeServices.EmailGrpc;
using Ozon.MerchandiseService.Infrastructure.Commands;

namespace Ozon.MerchandiseService.Infrastructure.Handlers
{
    public class CreateNewIssueMerchHandler: IRequestHandler<CreateNewIssueMerchCommand, MerchIssue>
    {
        private readonly IMerchIssueRepository _merchIssueRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public CreateNewIssueMerchHandler(IMerchIssueRepository merchIssueRepository, IEmployeeRepository employeeRepository)
        {
            _merchIssueRepository = merchIssueRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<MerchIssue> Handle(CreateNewIssueMerchCommand request, CancellationToken cancellationToken)
        {
            MerchIssue merchIssue = null;
            
            //Создаем объект выдачи мерча
            var employee = _employeeRepository.GetById(request.EmployeeId);
            var newId = _merchIssueRepository.Count() + 1;
            if (employee != null)
            {
                merchIssue = new MerchIssue(
                    newId,
                    employee,
                    new MerchType() {Value = Enumeration.FromValue<MerchTypeEnum>(request.MerchType)}
                );
                _merchIssueRepository.Add(merchIssue);
            }
            
            if (merchIssue != null)
            {
                if (merchIssue.IssueStatus.Id == IssueStatusEnum.Created.Id ||
                    merchIssue.IssueStatus.Id == IssueStatusEnum.InQueue.Id)
                {
                    StockGrpcFakeService stockApi = new StockGrpcFakeService();

                    var issueRequest = new IssueMerchRequest() {MerchPackType = merchIssue.MerchPackType.Value.Id};
                    
                    if (stockApi.IssueMerchRequest(issueRequest))
                       merchIssue.SetPendingStatus(); //Ожидает выдачи сотруднику
                    else
                        merchIssue.SetInQueueStatus();
                }
                
                //Если готов к выдаче, отправляем email
                if (merchIssue.IssueStatus.Id == IssueStatusEnum.PendingIssue.Id)
                {
                    EmailFakeGrpcService emailService = new EmailFakeGrpcService();
                    emailService.SendEmail(new SendEmailRequest()
                    {
                        EmployeeId = merchIssue.Employee.Id,
                        MerchIssueId = merchIssue.Id,
                        MerchPackType = merchIssue.MerchPackType.Value.Id
                    });
                }
            }
            
            _merchIssueRepository.Save();
            
            return await Task.FromResult(merchIssue);
        }
    }
}