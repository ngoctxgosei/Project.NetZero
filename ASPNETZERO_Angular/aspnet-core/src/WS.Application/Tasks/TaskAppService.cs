using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WS.Authorization;
using WS.Tasks.Configuration;
using WS.Tasks.Dto;

namespace WS.Tasks
{
    public class TaskAppService : WSAppServiceBase, ITaskAppService
    {
        private readonly IRepository<Task> _taskRepository;
        public TaskAppService(IRepository<Task> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<ListResultDto<TaskListDto>> GetAll(GetAllTasksInput input)
        {
            var tasks = await _taskRepository
                .GetAll()
                .WhereIf(input.TaskState.HasValue, task => task.State == input.TaskState.Value)
                .OrderByDescending(task =>task.CreationTime)
                .ToListAsync();

                var dtos = ObjectMapper.Map<List<TaskListDto>>(tasks);
                return new ListResultDto<TaskListDto>(dtos);
        }

        public async Task<GetTaskForEditOutput> GetTaskForEdit(NullableIdDto input)
        {
            TaskEditDto dto;

            if (input.Id.HasValue) //Editing existing role?
            {
                var task = await _taskRepository.FirstOrDefaultAsync(input.Id.Value);
                //taskEditDto = ObjectMapper.Map<TaskEditDto>(task);
                 dto = ObjectMapper.Map<TaskEditDto>(task);
            }
            else
            {
                dto = new TaskEditDto();
            }

            return new GetTaskForEditOutput
            {
                Task = dto
            };
        }

        public async Task<TaskListDto> UpdateTask(TaskEditDto input)
        {
            Logger.Info("Updating a task for input: " + input);

            var task = await _taskRepository.FirstOrDefaultAsync(input.Id.Value);
            task.Title = input.Title;
            task.Description = input.Description;
            task.State = input.State;
            task.TenantId = (int)AbpSession.TenantId;
            await _taskRepository.UpdateAsync(task);
            await CurrentUnitOfWork.SaveChangesAsync();
            if (task == null)
            {
                throw new UserFriendlyException(L("CouldNotFindTheTaskMessage"));
            }
            return ObjectMapper.Map<TaskListDto>(task);
        }
       
        public async Task<TaskListDto> Create(TaskEditDto input)
        {
            var task = ObjectMapper.Map<Task>(input);
            task.TenantId =(int)AbpSession.TenantId;
            await _taskRepository.InsertAsync(task);
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<TaskListDto>(task);
        }
        //public async Task<TaskListDto> DeleteTask(EntityDto<int> input)
        //{
        //     await _taskRepository.DeleteAsync(input.Id);
        //}
        public async Task<TaskListDto> DeleteTask(EntityDto<int> input)
        {
            var task = await _taskRepository.FirstOrDefaultAsync(input.Id);
            await _taskRepository.DeleteAsync(input.Id);
            return ObjectMapper.Map<TaskListDto>(task);
        }

        public class CreateTaskInput
        {
            [Required]
            [MaxLength(TaskEntityConfiguration.TitleMaxLength)]
            public string Title { get; set;}
            [MaxLength(TaskEntityConfiguration.DescriptionMaxLength)]
            public string Description { get; set; }
            public int? TenantId { get; set; }
            public TaskState State { get; set; }
        }

       
        public class UpdateTaskInput
        {
            [Range(1, long.MaxValue)]
            public int Id { get; set; }
            [Required]
            [MaxLength(TaskEntityConfiguration.TitleMaxLength)]
            public string Title { get; set; }
            [MaxLength(TaskEntityConfiguration.DescriptionMaxLength)]
            public string Description { get; set; }
            public int? TenantId { get; set; }
            public TaskState State { get; set; }
        }
    }


}
