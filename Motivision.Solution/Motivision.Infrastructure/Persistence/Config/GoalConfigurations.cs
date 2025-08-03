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
    public class GoalConfigurations : IEntityTypeConfiguration<Goal>
    {
        public void Configure(EntityTypeBuilder<Goal> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Title)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(g => g.Description)
                   .HasMaxLength(500);

            builder.Property(g => g.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(e => e.LastUpdated)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(g => g.UserId)
                   .IsRequired();

            // One Goal has many Steps
            builder.HasMany(g => g.Steps)
                   .WithOne(s => s.Goal)
                   .HasForeignKey(s => s.GoalId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
