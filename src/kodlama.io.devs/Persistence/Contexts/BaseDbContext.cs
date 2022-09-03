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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // if(!optionsBuilder.IsConfigured)
        //     base.OnConfiguring(optionsBuilder.UseSqlServer(Configuration.GetConnectionString("ProgrammingLanguageConnectionString")));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProgrammingLanguage>(l =>
        {
            l.ToTable("ProgrammingLanguages").HasKey(k => k.Id);
            l.Property(p => p.Id).HasColumnName("Id");
            l.Property(p => p.Name).HasColumnName("Name");
        });

        ProgrammingLanguage[] programmingLanguageSeeds = { new(1, "C#"), new(2, "Java") };
        modelBuilder.Entity<ProgrammingLanguage>().HasData(programmingLanguageSeeds);
    }
}