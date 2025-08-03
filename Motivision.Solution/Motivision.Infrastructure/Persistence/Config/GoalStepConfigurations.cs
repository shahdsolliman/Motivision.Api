using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Motivision.Core.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Infrastructure.Persistence.Config
{
    public class GoalStepConfigurations : IEntityTypeConfiguration<GoalStep>
    {
        public void Configure(EntityTypeBuilder<GoalStep> builder)
        {
            builder.Property(s => s.Title)
            .IsRequired()
            .HasMaxLength(100);

            builder.Property(s => s.Description)
                .HasMaxLength(500);

            builder.HasOne(s => s.FocusSession)
                .WithOne(fs => fs.GoalStep)
                .HasForeignKey<GoalStep>(s => s.FocusSessionId)
                .OnDelete(DeleteBehavior.SetNull);


        }
    }
}