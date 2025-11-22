using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistences.EntityConfigurations;

public class TestSessionConfigurations : IEntityTypeConfiguration<TestSession>
{
    public void Configure(EntityTypeBuilder<TestSession> builder)
    {
        builder.ToTable("TestSessions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.StartedAt)
            .IsRequired();

        builder.Property(x => x.CompletedAt)
            .IsRequired(false);

        builder.Property(x => x.ScorePercent)
            .HasColumnType("decimal(5,2)")
            .IsRequired();

        builder.Property(x => x.CorrectAnswersCount)
            .IsRequired();

        builder.Property(x => x.TotalQuestions)
            .IsRequired();

        builder.Property(x => x.IsPassed)
            .IsRequired();

        builder.Property(x => x.TopicId)
            .IsRequired(false);

        builder.Property(x => x.SectionId)
            .IsRequired();

        builder.Property(x => x.UserId)
            .IsRequired();

        // TestSession → Topic
        builder.HasOne(x => x.Topic)
            .WithMany(t => t.TestSessions)
            .HasForeignKey(x => x.TopicId)
            .OnDelete(DeleteBehavior.Cascade);

        // TestSession → Section
        builder.HasOne(x => x.Section)
            .WithMany(s => s.TestSessions)
            .HasForeignKey(x => x.SectionId)
            .OnDelete(DeleteBehavior.Cascade);

        // TestSession → User
        builder.HasOne(x => x.User)
            .WithMany(u => u.TestSessions)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.TopicId);
        builder.HasIndex(x => x.SectionId);
        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => new { x.UserId, x.TopicId });
      
    }
}
