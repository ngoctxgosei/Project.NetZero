using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Dto;
using WS.Employees.Dto;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Abp.UI;

namespace WS.Employees
{
    public class EmployeeAppService : WSAppServiceBase, IEmployeeAppService
    {
        private readonly IRepository<Employee> _empRepository;
        public EmployeeAppService(IRepository<Employee> empRepository)
        {
            _empRepository = empRepository;
        }
        //public async Task<PagedResultDto<EmployeeListDto>> GetEmployees(GetEmployeeInput input)
        public async Task<ListResultDto<EmployeeListDto>> GetEmployees(GetEmployeeInput input)
        {
            var emps = await _empRepository
                .GetAll()
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(),
                               u => u.Name.Contains(input.Filter))
                .OrderByDescending(u => u.Birthday)
                .ToListAsync();

            var dtos = ObjectMapper.Map<List<EmployeeListDto>>(emps);
            return new ListResultDto<EmployeeListDto>(dtos);
        }
        public async Task<GetEmployeeForEditOutput> GetEmployeeForEdit(NullableIdDto input)
        {
            EmployeeEditDto dto;

            if (input.Id.HasValue) //Editing existing role?
            {
                var emp = await _empRepository.FirstOrDefaultAsync(input.Id.Value);
                //taskEditDto = ObjectMapper.Map<TaskEditDto>(task);
                dto = ObjectMapper.Map<EmployeeEditDto>(emp);
            }
            else
            {
                dto = new EmployeeEditDto();
            }

            return new GetEmployeeForEditOutput
            {
                Employee = dto
            };
        }

        public async Task CreateOrUpdateEmployee(CreateOrUpdateEmployeeInput input)
        {
            if (input.Employee.Id.HasValue)
            {
                await UpdateEmployeeAsync(input);
            }
            else
            {
                await CreateEmployeeAsync(input);
            }
        }

        protected virtual async Task UpdateEmployeeAsync(CreateOrUpdateEmployeeInput input)
        {
            Logger.Info("Updating a task for input: " + input);

            var emp = await _empRepository.FirstOrDefaultAsync(input.Employee.Id.Value);
            emp.Name = input.Employee.Name;
            emp.Birthday = input.Employee.Birthday;
            emp.TenantId = (int)AbpSession.TenantId;
            await _empRepository.UpdateAsync(emp);
            await CurrentUnitOfWork.SaveChangesAsync();
            if (emp == null)
            {
                throw new UserFriendlyException(L("CouldNotFindTheTaskMessage"));
            }
            //return ObjectMapper.Map<EmployeeListDto>(emp);
        }

        protected virtual async Task CreateEmployeeAsync(CreateOrUpdateEmployeeInput input)
        {

            var emp = ObjectMapper.Map<Employee>(input.Employee); //Passwords is not mapped (see mapping configuration)
            emp.TenantId = (int)AbpSession.TenantId;
            await _empRepository.InsertAsync(emp);
            await CurrentUnitOfWork.SaveChangesAsync();

            //return ObjectMapper.Map<TaskListDto>(task);
        }

        public async Task<EmployeeListDto> DeleteEmployee(EntityDto<int> input)
        {
            var task = await _empRepository.FirstOrDefaultAsync(input.Id);
            await _empRepository.DeleteAsync(input.Id);
            return ObjectMapper.Map<EmployeeListDto>(task);


            //public async Task<FileDto> GetEmployeesToExcel()
            //{
            //    var users = await UserManager.Users.ToListAsync();
            //    var userListDtos = ObjectMapper.Map<List<UserListDto>>(users);
            //    await FillRoleNames(userListDtos);

            //    return _userListExcelExporter.ExportToFile(userListDtos);
            //}
        }
    }
}
