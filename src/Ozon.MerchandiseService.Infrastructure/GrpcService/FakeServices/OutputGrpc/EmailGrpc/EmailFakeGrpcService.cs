using System;

namespace Ozon.MerchandiseService.GrpcService.FakeServices.EmailGrpc
{
    /// <summary>
    /// Для симуляции работы Email Service (только входящие в него вызовы)
    /// </summary>
    public class EmailFakeGrpcService
    {
        public void SendEmail(SendEmailRequest request)
        {
            //Отправляется инфа о готовности в Email Service
            Console.WriteLine($"Сотрудник №{request.EmployeeId} MerchPack №{request.MerchPackType} ожидает получения.");
        }
    }

    public class SendEmailRequest
    {
        public long EmployeeId { get; set; }
        public int MerchPackType { get; set; }
    }
}