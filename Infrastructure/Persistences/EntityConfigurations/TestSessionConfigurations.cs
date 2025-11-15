using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistences.EntityConfigurations;

public class TestSessionConfigurations : IEntityTypeConfiguration<TestSession>
{
    public void Configure(EntityTypeBuilder<TestSession> builder)
    {
        // Таблица
        builder.ToTable("TestSessions");

        // Первичный ключ
        builder.HasKey(x => x.Id);

        // Свойства
        builder.Property(x => x.StartedAt)
            .IsRequired();

        builder.Property(x => x.CompletedAt)
            .IsRequired(false); // null - если тест не закончен

        builder.Property(x => x.ScorePercent)
            .HasColumnType("decimal(5,2)") 
            .IsRequired();
            // 99.99 максимум — более чем достаточно

        builder.Property(x => x.CorrectAnswersCount)
            .IsRequired();

        builder.Property(x => x.TotalQuestions)
            .IsRequired();

        builder.Property(x => x.IsPassed)
            .IsRequired();

        builder.HasOne(x => x.Topic)
            .WithMany()              
            .HasForeignKey(x => x.TopicId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.User)
            .WithMany()                 
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
            

        builder.HasIndex(x => x.TopicId);
        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => new { x.UserId, x.TopicId });
      
    }
}
