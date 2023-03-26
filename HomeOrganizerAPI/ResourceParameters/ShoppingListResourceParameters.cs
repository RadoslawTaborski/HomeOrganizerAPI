namespace HomeOrganizerAPI.ResourceParameters;

public class ShoppingListResourceParameters : DefaultParameters
{
    public override int MaxPageSize { get; set; } = 50;
    public override int PageSize { get; set; } = 25;
    public string Name { get; set; }
    public string CategoryUuid { get; set; }
}
