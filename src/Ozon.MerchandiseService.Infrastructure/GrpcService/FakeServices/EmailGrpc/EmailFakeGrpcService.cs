using System;

namespace Ozon.MerchandiseService.GrpcService.FakeServices.EmailGrpc
{
    public class EmailFakeGrpcService
    {
        public void SendEmail(SendEmailRequest request)
        {
            //Отправляется инфа о готовности в Email Service
        }
    }

    public class SendEmailRequest
    {
        public int MerchIssueId { get; set; }
        public int EmployeeId { get; set; }
        public int MerchPackType { get; set; }
    }
}