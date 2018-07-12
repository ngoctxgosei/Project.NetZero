using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;
using System.Collections.Generic;
using System.Text;
using Abp.Extensions;
namespace WS.Employees
{
    public class Employee : Entity, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public string Name { get; set; }
        public DateTime Birthday  { get; set; }
    }
    //public Employee()
    //{
    //    Birthday = Clock.Birthday;
    //}
    //public Employee(int tenantid, string title, string description = null) : this()
    //{
    //    TenantId = tenantid;
    //    Name = title;
    //    Description = description;
    //}

}
