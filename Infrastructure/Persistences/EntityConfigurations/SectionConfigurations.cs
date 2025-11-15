using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistences.EntityConfigurations;

public class SectionConfigurations : IEntityTypeConfiguration<Section>
{
    public void Configure(EntityTypeBuilder<Section> builder)
    {
        builder.ToTable("Sections");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.HasMany(x => x.Topics)
            .WithOne(t => t.Section)
            .HasForeignKey(t => t.SectionId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasMany(x => x.TestSessions)
            .WithOne(ts => ts.Section)
            .HasForeignKey(ts => ts.SectionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.IsActive);
        builder.HasIndex(x => x.Name);
    }
}
