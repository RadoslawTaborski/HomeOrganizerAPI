namespace HomeOrganizerAPI.ResourceParameters;

public class ExpenseDetialsResourceParameters : Parameters
{
    public override int MaxPageSize { get; set; } = 50;
    public override int PageSize { get; set; } = 50;
    public string ExpenseUuid { get; set; }
    public string GroupUuid { get; set; }
}
