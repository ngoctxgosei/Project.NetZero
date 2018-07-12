using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Tasks.Configuration
{
    public class TaskEntityConfiguration
    {
        public const int TitleMaxLength = 256 ;
        public const int DescriptionMaxLength = 64 * 1024 ;


        public static void Configure(EntityTypeBuilder<Task> entityBuilder)
        {
            entityBuilder.ToTable("Tasks");
            entityBuilder.Property(e => e.Title).IsRequired().HasMaxLength(TitleMaxLength);
            entityBuilder.Property(e => e.Description).IsRequired().HasMaxLength(DescriptionMaxLength);
        }
    }
}
