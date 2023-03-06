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
    internal class MovieProducerConfiguration : IEntityTypeConfiguration<MovieProducer>
    {
        public void Configure(EntityTypeBuilder<MovieProducer> builder)
        {
            builder.ToTable(nameof(MovieProducer));
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.ProducerId).IsRequired();
            builder.Property(p => p.MovieId).IsRequired();

            builder.HasOne(p => p.Producer).WithMany(p => p.Movies).HasForeignKey(p => p.ProducerId);
            builder.HasOne(p => p.Movie).WithMany(p => p.Producers).HasForeignKey(p => p.MovieId);

        }
    }
}
