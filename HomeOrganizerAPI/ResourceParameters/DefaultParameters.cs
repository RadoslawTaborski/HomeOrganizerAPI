namespace HomeOrganizerAPI.ResourceParameters
{
    public class DefaultParameters : Parameters
    {
        public override int MaxPageSize { get; set; } = 50;
        protected override int PageSize { get; set; } = 50;
    }
}
