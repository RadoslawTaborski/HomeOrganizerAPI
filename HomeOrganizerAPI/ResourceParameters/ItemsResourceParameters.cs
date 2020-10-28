namespace HomeOrganizerAPI.ResourceParameters
{
    public class ItemsResourceParameters : Parameters
    {
        public override int MaxPageSize { get; set; } = 50;
        protected override int PageSize { get; set; } = 25;
        public string GroupUuid { get; set; }
        public string CategoryUuid { get; set; }
        public string SubcategoryUuid { get; set; }
        public int StateLevel { get; set; } = 2;
    }
}