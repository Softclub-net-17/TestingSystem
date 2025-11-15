using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistences.EntityConfigurations;

public class QuestionConfigurations: IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("Questions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Text)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.TopicId)
            .IsRequired();

        builder.Property(x => x.SectionId)
            .IsRequired();

        // Question → Topic
        builder.HasOne(x => x.Topic)
            .WithMany(t => t.Questions)
            .HasForeignKey(x => x.TopicId)
            .OnDelete(DeleteBehavior.Cascade);

        // Question → Section
        builder.HasOne(x => x.Section)
            .WithMany(s => s.Questions)
            .HasForeignKey(x => x.SectionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.AnswerOptions)
            .WithOne(a => a.Question)
            .HasForeignKey(a => a.QuestionId);

        builder.HasIndex(x => x.TopicId);
        builder.HasIndex(x => x.SectionId);
        builder.HasIndex(x => new { x.TopicId, x.IsActive });
    }
}
