namespace HomeOrganizerAPI.Helpers.DTO
{
    public record ShoppingList : DtoModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Visible { get; set; }
    }
}
