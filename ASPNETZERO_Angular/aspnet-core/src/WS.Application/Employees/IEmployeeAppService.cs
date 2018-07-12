using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WS.Dto;
using WS.Employees.Dto;

namespace WS.Employees
{
    public interface IEmployeeAppService: IApplicationService
    {
        Task<ListResultDto<EmployeeListDto>> GetEmployees(GetEmployeeInput input);

        //Task<FileDto> GetEmployeeToExcel();

        Task<GetEmployeeForEditOutput> GetEmployeeForEdit(NullableIdDto input);

        //Task CreateOrUpdateEmployee(CreateOrUpdateEmployeeInput input);

        //Task DeleteEmployee(EntityDto<long> input);
    }
}
