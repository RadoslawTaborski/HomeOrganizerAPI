namespace HomeOrganizerAPI.ResourceParameters
{
    public abstract class Parameters
    {
        protected abstract int PageSize { get; set; }

        public abstract int MaxPageSize { get; set; }
        public int PageNumber { get; set; } = 1;

        public int DefaultPageSize
        {
            get => PageSize;
            set => PageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string OrderBy { get; set; } = "uuid";
    }
}
