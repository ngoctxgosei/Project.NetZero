using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using WS.Dto;

namespace WS.Employees.Dto
{
    public class GetEmployeeInput: PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Name";
            }
        }
    }
}
