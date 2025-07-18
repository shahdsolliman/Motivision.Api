using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Motivision.Core.Business.Entities;
using Motivision.Core.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Infrastructure.Persistence.Config
{
    public class SkillConfigurations : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(s => s.Category)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(s => s.ProgressPercentage)
                   .HasDefaultValue(0);

            builder.Property(s => s.LastUpdated)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Relationship with FocusSessions
            builder.HasMany(s => s.FocusSessions)
                   .WithOne(fs => fs.Skill)
                   .HasForeignKey(fs => fs.SkillId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
