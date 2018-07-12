using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using WS.Auditing.Dto;
using WS.Dto;

namespace WS.Auditing
{
    public interface IAuditLogAppService : IApplicationService
    {
        Task<PagedResultDto<AuditLogListDto>> GetAuditLogs(GetAuditLogsInput input);

        Task<FileDto> GetAuditLogsToExcel(GetAuditLogsInput input);
    }
}