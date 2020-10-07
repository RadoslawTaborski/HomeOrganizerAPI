using System;

namespace HomeOrganizerAPI.Helpers.DTO
{
    public record Item : DtoModel
    {
        public string Name { get; set; }
        public int? ShoppingListId { get; set; }
        public int? StateId { get; set; }
        public string Quantity { get; set; }
        public int CategoryId { get; set; }
        public DateTimeOffset? Bought { get; set; }
    }
}
