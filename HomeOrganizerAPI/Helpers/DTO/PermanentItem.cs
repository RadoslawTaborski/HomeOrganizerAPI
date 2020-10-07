namespace HomeOrganizerAPI.Helpers.DTO
{
    public record PermanentItem : DtoModel
    {
        public string Name { get; set; }
        public int? StateId { get; set; }
        public int CategoryId { get; set; }
    }
}
