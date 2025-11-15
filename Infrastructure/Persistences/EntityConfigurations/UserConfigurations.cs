using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistences.EntityConfigurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        
        builder.HasKey(user => user.Id);
        
        builder.Property(user => user.Email)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(user => user.PasswordHash)
            .IsRequired();
        
        
    }
}