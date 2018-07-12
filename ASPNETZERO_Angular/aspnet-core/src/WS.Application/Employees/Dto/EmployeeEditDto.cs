using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WS.Employees.Dto
{
   public class EmployeeEditDto
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int TenantId { get; set; }
        public DateTime Birthday { get; set; }

    }
}
