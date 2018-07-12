using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WS.Tasks.Dto;

namespace WS.Tasks
{
    public interface ITaskAppService : IApplicationService
    {
        Task<ListResultDto<TaskListDto>> GetAll(GetAllTasksInput input);
    }


    public class GetAllTasksInput
    {
        public TaskState? TaskState { get; set; }
    }
    //Task<GetTaskForEditOutput> GetTaskForEdit(NullableIdDto input);

    [AutoMapFrom(typeof(Task))]
    public class TaskListDto : EntityDto, IHasCreationTime
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set;}
        public TaskState State { get; set;}
        public int TenantId { get; set; }
    }

}
