using HomeOrganizerAPI.ResourceParameters;

namespace HomeOrganizerAPI.ResourceParameters
{
    public class PermanentItemsResourceParameters : Parameters
    {
        public override int MaxPageSize { get; set; } = 50;
        protected override int DefaultPageSize { get; set; } = 15;
        public string CategoryId { get; set; }
        public string SubcategoryId { get; set; }
        public string StateId { get; set; }
    }
}