
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Employees.Configuration
{
    public class EmployeeEntityConfiguration
    {
        public const int NameMaxLength = 32;
        public static void Configure(EntityTypeBuilder<Employee> entityBuilder)
        {
            entityBuilder.ToTable("AbpEmployee");
            entityBuilder.Property(e => e.Name).IsRequired().HasMaxLength(NameMaxLength);
        }
    }
}

