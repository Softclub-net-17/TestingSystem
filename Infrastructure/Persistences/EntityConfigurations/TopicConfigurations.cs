using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistences.EntityConfigurations;

public class TopicConfigurations : IEntityTypeConfiguration<Topic>
{
    public void Configure(EntityTypeBuilder<Topic> builder)
    {
        builder.ToTable("Topics");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Content)
            .IsRequired()
            .HasColumnType("text");

        builder.Property(x => x.IsPublished)
            .IsRequired();

        builder.Property(x => x.SectionId)
            .IsRequired();

        // Topic → Section
        builder.HasOne(x => x.Section)
            .WithMany(s => s.Topics)
            .HasForeignKey(x => x.SectionId)
            .OnDelete(DeleteBehavior.Cascade);

        // Topic → Questions
        builder.HasMany(x => x.Questions)
            .WithOne(q => q.Topic)
            .HasForeignKey(q => q.TopicId)
            .OnDelete(DeleteBehavior.Cascade);

        // Topic → TestSessions
        builder.HasMany(x => x.TestSessions)
            .WithOne(ts => ts.Topic)
            .HasForeignKey(ts => ts.TopicId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.SectionId);
        builder.HasIndex(x => x.IsPublished);
    }
}
