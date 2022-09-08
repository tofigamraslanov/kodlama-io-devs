using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class GitHubProfileConfiguration : IEntityTypeConfiguration<GitHubProfile>
{
    public void Configure(EntityTypeBuilder<GitHubProfile> builder)
    {
        builder.ToTable("GitHubProfiles").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.DeveloperId).HasColumnName("DeveloperId");
        builder.Property(p => p.GitHubAddress).HasColumnName("GitHubAddress");
        builder.HasOne(p => p.Developer);
    }
}