namespace HomeOrganizerAPI.ResourceParameters;

public class UsersResourceParameters : Parameters
{
    public override int MaxPageSize { get; set; } = 50;
    public override int PageSize { get; set; } = 25;
    public string GroupUuid { get; set; }
}
