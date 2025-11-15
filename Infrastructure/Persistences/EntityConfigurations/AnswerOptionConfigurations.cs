using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistences.EntityConfigurations;

public class AnswerOptionConfigurations : IEntityTypeConfiguration<AnswerOption>
{
    public void Configure(EntityTypeBuilder<AnswerOption> builder)
    {
        builder.ToTable("AnswerOptions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Text)
            .IsRequired()
            .HasMaxLength(500); 

        builder.Property(x => x.IsCorrect)
            .IsRequired();

        builder.HasOne(x => x.Question)
            .WithMany(q => q.AnswerOptions)
            .HasForeignKey(x => x.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.QuestionId);
    }
}
