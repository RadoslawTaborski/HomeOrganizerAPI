namespace HomeOrganizerAPI.ResourceParameters;

public class SubcategoryResourceParameters : Parameters
{
    public override int MaxPageSize { get; set; } = 50;
    public override int PageSize { get; set; } = 50;
    public string CategoryUuid { get; set; }
    public string GroupUuid { get; set; }
}
