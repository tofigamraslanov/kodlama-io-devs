using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class TechnologyConfiguration:IEntityTypeConfiguration<Technology>
{
    public void Configure(EntityTypeBuilder<Technology> builder)
    {
        builder.ToTable("Technologies").HasKey(e => e.Id);

        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.ProgrammingLanguageId).HasColumnName("ProgrammingLanguageId");
        builder.Property(p => p.Name).HasColumnName("Name");

        builder.HasOne(t => t.ProgrammingLanguage);
    }
}