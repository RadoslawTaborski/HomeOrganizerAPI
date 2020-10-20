namespace HomeOrganizerAPI.ResourceParameters
{
    public class PermanentItemsResourceParameters : Parameters
    {
        public override int MaxPageSize { get; set; } = 50;
        protected override int PageSize { get; set; } = 25;
        public string GroupId { get; set; }
        public string CategoryId { get; set; }
        public string SubcategoryId { get; set; }
        public string StateId { get; set; }
    }
}