using System.Collections.Generic;

namespace Ozon.MerchandiseService.HttpModels
{
    public class MerchIssueDto
    {
        /// <summary>
        /// ID сотрудника
        /// </summary>
        public long EmployeeId { get; }
        
        public List<MerchIssueItemDto> MerchIssueItemDtos { get; }
        public MerchIssueDto(long employeeId)
        {
            EmployeeId = employeeId;
            MerchIssueItemDtos = new List<MerchIssueItemDto>();
        }

        public void AddMerchIssueItemDto(MerchIssueItemDto issueItemDto)
        {
            MerchIssueItemDtos.Add(issueItemDto);
        }
    }

    public class MerchIssueItemDto
    {
        /// <summary>
        /// Статус заявки
        /// </summary>
        public string IssueStatus { get; }
        /// <summary>
        /// Тип мерч пака
        /// </summary>
        public string MerchPack { get; }
        public MerchIssueItemDto(string merchPack, string issueStatus)
        {
            MerchPack = merchPack;
            IssueStatus = issueStatus;
        }
    }
}