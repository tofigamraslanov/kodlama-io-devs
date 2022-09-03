using Core.Persistence.Repositories;

namespace Domain.Entities;

public class ProgrammingLanguage : Entity
{
    public string Name { get; set; } = null!;

    public ProgrammingLanguage()
    {
    }

    public ProgrammingLanguage(int id, string name) : this()
    {
        Id = id;
        Name = name;
    }
}