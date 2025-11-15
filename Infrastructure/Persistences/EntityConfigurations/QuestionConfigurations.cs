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
        
        builder.HasOne(x => x.Topic)
            .WithMany(t => t.Questions)
            .HasForeignKey(x => x.TopicId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.TopicId);
    }
}
