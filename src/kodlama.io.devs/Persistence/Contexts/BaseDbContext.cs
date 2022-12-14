using System.Reflection;
using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.Contexts;

public class BaseDbContext : DbContext
{
    protected IConfiguration Configuration { get; set; }

    public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
    {
        Configuration = configuration;
    }

    public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; } = null!;
    public DbSet<Technology> Technologies { get; set; } = null!;
    public DbSet<GitHubProfile> GitHubProfiles { get; set; } = null!;

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<OperationClaim> OperationClaims { get; set; } = null!;
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; } = null!;
    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        ProgrammingLanguage[] programmingLanguageSeeds =
        {
            new(1, "C#"),
            new(2, "Java")
        };
        modelBuilder.Entity<ProgrammingLanguage>().HasData(programmingLanguageSeeds);

        Technology[] technologySeeds =
        {
            new(1, 1, "Wpf"),
            new(2, 1, "Asp.Net Core"),
            new(3, 2, "Spring")
        };
        modelBuilder.Entity<Technology>().HasData(technologySeeds);

        OperationClaim[] operationClaimSeeds =
        {
            new(1, "Admin"),
            new(2, "User")
        };
        modelBuilder.Entity<OperationClaim>().HasData(operationClaimSeeds);
    }
}