using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Employees.Dto
{
    public class EmployeeListDto : EntityDto<long>
    {
        public string Name { get; set; }

        public string TenantId { get; set; }

        public DateTime Birthday { get; set; }

    }
}
