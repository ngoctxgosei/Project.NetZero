using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WS.Tasks.Dto
{
    [AutoMapFrom(typeof(Task))]
    public class TaskEditDto
    {
        public int? Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
        public TaskState State { get; set; }
        public int TenantId { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
