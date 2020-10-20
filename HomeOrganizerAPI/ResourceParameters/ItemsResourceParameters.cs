namespace HomeOrganizerAPI.ResourceParameters
{
    public class ItemsResourceParameters : Parameters
    {
        public override int MaxPageSize { get; set; } = 50;
        protected override int PageSize { get; set; } = 25;
        public string GroupId { get; set; }
        public string CategoryId { get; set; }
        public string SubcategoryId { get; set; }
        public int StateId { get; set; } = 2;
    }
}