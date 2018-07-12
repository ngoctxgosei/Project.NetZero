using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Tasks
{
    public class Task: Entity, IMustHaveTenant, IHasCreationTime
    {
       
        public int TenantId { get; set;}
        public string Title { get; set;}
        public string Description { get; set;}
        public DateTime CreationTime { get; set;}
        public TaskState State { get; set;}

        public Task()
        {
            CreationTime = Clock.Now;
            State = TaskState.Open;
        }

        public Task( int tenantid, string title, string description = null) : this ()
        {
            TenantId = tenantid;
            Title = title;
            Description = description;
        }
    }
}
