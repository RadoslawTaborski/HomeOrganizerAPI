namespace HomeOrganizerAPI.ResourceParameters
{
    public class TemporaryItemsResourceParameters : Parameters
    {
        public override int MaxPageSize { get; set; } = 50;
        public override int PageSize { get; set; } = 25;
        public string GroupUuid { get; set; }
        public string CategoryUuid { get; set; }
        public string SubcategoryUuid { get; set; }
        public string ShoppingListUuid { get; set; }
    }
}
