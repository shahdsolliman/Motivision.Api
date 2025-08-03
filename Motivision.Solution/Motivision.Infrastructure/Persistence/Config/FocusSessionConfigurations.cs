using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Motivision.Core.Business.Entities;
using Motivision.Core.Business.Enums;
using Motivision.Core.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Infrastructure.Persistence.Config
{
    public class FocusSessionConfigurations : IEntityTypeConfiguration<FocusSession>
    {
        public void Configure(EntityTypeBuilder<FocusSession> builder)
        {
            builder.HasKey(fs => fs.Id);

            builder.Property(fs => fs.Notes)
                   .HasMaxLength(500);

            builder.Property(s => s.SessionType)
                   .HasConversion(v => v.ToString(), 
                   v => (SessionType)Enum.Parse(typeof(SessionType), v));

            builder.Property(s => s.SessionCategory)
                   .HasConversion(v => v.ToString(),
                   v => (SessionCategory)Enum.Parse(typeof(SessionCategory), v));

        }
    }
}
