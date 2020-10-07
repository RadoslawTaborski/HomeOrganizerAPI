namespace HomeOrganizerAPI.Helpers.DTO
{
    public record Expenses : DtoModel
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public int PayerId { get; set; }
        public int RecipientId { get; set; }
    }
}
