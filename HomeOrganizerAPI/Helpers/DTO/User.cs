using System;

namespace HomeOrganizerAPI.Helpers.DTO
{
    public record User : DtoModel
    {
        public string Username { get; init; }
        public string Email { get; init; }
        public UserGroups[] UserGroups { get; init; }

        public static implicit operator User(Models.User v)
        {
            throw new NotImplementedException();
        }
    }
}
