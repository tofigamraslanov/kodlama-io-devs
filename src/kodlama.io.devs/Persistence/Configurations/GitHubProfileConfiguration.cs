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
        builder.Property(p => p.UserId).HasColumnName("UserId");
        builder.Property(p => p.Login).HasColumnName("Login");
        builder.Property(p => p.HtmlUrl).HasColumnName("HtmlUrl");
        builder.Property(p => p.Name).HasColumnName("Name");
        builder.Property(p => p.Company).HasColumnName("Company");
        builder.Property(p => p.Blog).HasColumnName("Blog");
        builder.Property(p => p.Location).HasColumnName("Location");
        builder.Property(p => p.PublicRepos).HasColumnName("PublicRepos");
        builder.Property(p => p.Followers).HasColumnName("Followers");
        builder.Property(p => p.Following).HasColumnName("Following");

        builder.HasOne(p => p.User);
    }
}