using System;
using System.Collections.Generic;

namespace HomeOrganizerAPI.Services;

public class PropertyMapping<TSrc, TDst> : IPropertyMapping
{
    public Dictionary<string, PropertyMappingValue> MappingDictionary { get; private set; }

    public PropertyMapping(Dictionary<string, PropertyMappingValue> mappingDictionary)
    {
        MappingDictionary = mappingDictionary ?? throw new ArgumentNullException(nameof(mappingDictionary));
    }
}
