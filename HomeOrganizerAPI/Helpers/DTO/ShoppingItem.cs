using System;

namespace HomeOrganizerAPI.Helpers.DTO
{
    public record ShoppingItem : DtoModel
    {
        public string Name { get; set; }
        public int? StateId { get; set; }
        public int CategoryId { get; set; }
        public int? ShoppingListId { get; set; }
        public string Quantity { get; set; }
        public DateTimeOffset? Bought { get; set; }
        public bool? Visible { get; set; }
        public DateTimeOffset? Archived { get; set; }
    }
}
