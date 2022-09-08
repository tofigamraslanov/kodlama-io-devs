namespace Application.Features.Technologies.Dtos;

public class UpdatedTechnologyDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int ProgrammingLanguageId { get; set; }
}