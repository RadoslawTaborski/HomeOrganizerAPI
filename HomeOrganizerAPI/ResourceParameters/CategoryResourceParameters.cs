namespace HomeOrganizerAPI.ResourceParameters;

public class ListCategoryResourceParameters : Parameters
{
    public override int MaxPageSize { get; set; } = 50;
    public override int PageSize { get; set; } = 50;
    public string GroupUuid { get; set; }
}
