using System.Collections.Generic;

namespace HomeOrganizerAPI.Services
{
    public interface IPropertyMappingService
    {
        Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSrc, TDst>();
    }
}