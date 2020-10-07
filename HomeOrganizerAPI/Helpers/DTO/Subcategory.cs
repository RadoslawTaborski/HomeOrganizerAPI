namespace HomeOrganizerAPI.Helpers.DTO
{
    public record Subcategory : DtoModel
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
    }
}
