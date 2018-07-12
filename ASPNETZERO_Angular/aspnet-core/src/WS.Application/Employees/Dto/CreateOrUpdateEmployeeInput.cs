using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WS.Employees.Dto
{
   public class CreateOrUpdateEmployeeInput
    {
        [Required]
        public EmployeeEditDto Employee { get; set; }
    }
}
