using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.FirstName).HasColumnName("FirstName");
        builder.Property(p => p.LastName).HasColumnName("LastName");
        builder.Property(p => p.Email).HasColumnName("Email");
        builder.Property(p => p.PasswordHash).HasColumnName("PasswordHash");
        builder.Property(p => p.PasswordSalt).HasColumnName("PasswordSalt");
        builder.Property(p => p.Status).HasColumnName("Status");
        builder.Property(p => p.AuthenticatorType).HasColumnName("AuthenticatorType");
        builder.HasMany(p => p.UserOperationClaims);
        builder.HasMany(p => p.RefreshTokens);
    }
}