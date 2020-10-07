namespace HomeOrganizerAPI.Helpers.DTO
{
    public record Saldo : DtoModel
    {
        public int PayerId { get; set; }
        public int RecipientId { get; set; }
        public decimal Value { get; set; }
    }
}
