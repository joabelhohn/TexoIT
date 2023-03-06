using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexoIt.Core.Entities;

namespace TexoIt.Infra.EntityFramework.Configurations
{
    internal class MovieStudioConfiguration : IEntityTypeConfiguration<MovieStudio>
    {
        public void Configure(EntityTypeBuilder<MovieStudio> builder)
        {
            builder.ToTable(nameof(MovieStudio));
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.StudioId).IsRequired();
            builder.Property(p => p.MovieId).IsRequired();

            builder.HasOne(p => p.Studio).WithMany(p => p.Movies).HasForeignKey(p => p.StudioId);
            builder.HasOne(p => p.Movie).WithMany(p => p.Studios).HasForeignKey(p => p.MovieId);

        }
    }
}
