namespace HomeOrganizerAPI.Helpers.DTO;

public record State : DtoModel
{
    public string Name { get; set; }
    public string Level { get; set; }
}
