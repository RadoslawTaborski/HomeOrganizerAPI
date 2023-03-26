using AutoMapper;
using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using CategoryDto = HomeOrganizerAPI.Helpers.DTO.Category;
using ListCategoryDto = HomeOrganizerAPI.Helpers.DTO.ListCategory;
using ExpensesDto = HomeOrganizerAPI.Helpers.DTO.Expenses;
using ExpenseDetailsDto = HomeOrganizerAPI.Helpers.DTO.ExpenseDetails;
using ExpensesSettingsDto = HomeOrganizerAPI.Helpers.DTO.ExpensesSettings;
using ItemDto = HomeOrganizerAPI.Helpers.DTO.Item;
using PermanentItemDto = HomeOrganizerAPI.Helpers.DTO.PermanentItem;
using SaldoDto = HomeOrganizerAPI.Helpers.DTO.Saldo;
using ShoppingItemDto = HomeOrganizerAPI.Helpers.DTO.ShoppingItem;
using ShoppingListDto = HomeOrganizerAPI.Helpers.DTO.ShoppingList;
using StateDto = HomeOrganizerAPI.Helpers.DTO.State;
using SubcategoryDto = HomeOrganizerAPI.Helpers.DTO.Subcategory;
using TemporaryItemDto = HomeOrganizerAPI.Helpers.DTO.TemporaryItem;
using UserDto = HomeOrganizerAPI.Helpers.DTO.User;
using GroupDto = HomeOrganizerAPI.Helpers.DTO.Group;

namespace HomeOrganizerAPI.Services;

public class PropertyMappingService : IPropertyMappingService
{
    private IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

    public PropertyMappingService()
    {
        MapperConfiguration mapperConfiguration = AutoMapperConfig.MapperConfiguration;
        _propertyMappings.Add(new PropertyMapping<ItemDto, Item>(CreatePropertyMapping<ItemDto, Item>(mapperConfiguration)));
        _propertyMappings.Add(new PropertyMapping<CategoryDto, Category>(CreatePropertyMapping<CategoryDto, Category>(mapperConfiguration)));
        _propertyMappings.Add(new PropertyMapping<ListCategoryDto, ListCategory>(CreatePropertyMapping<ListCategoryDto, ListCategory>(mapperConfiguration)));
        _propertyMappings.Add(new PropertyMapping<ExpensesDto, Expenses>(CreatePropertyMapping<ExpensesDto, Expenses>(mapperConfiguration)));
        _propertyMappings.Add(new PropertyMapping<ExpenseDetailsDto, ExpenseDetails>(CreatePropertyMapping<ExpenseDetailsDto, ExpenseDetails>(mapperConfiguration)));
        _propertyMappings.Add(new PropertyMapping<ExpensesSettingsDto, ExpensesSettings>(CreatePropertyMapping<ExpensesSettingsDto, ExpensesSettings>(mapperConfiguration)));
        _propertyMappings.Add(new PropertyMapping<PermanentItemDto, Item>(CreatePropertyMapping<PermanentItemDto, Item>(mapperConfiguration)));
        _propertyMappings.Add(new PropertyMapping<SaldoDto, Saldo>(CreatePropertyMapping<SaldoDto, Saldo>(mapperConfiguration)));
        _propertyMappings.Add(new PropertyMapping<ShoppingItemDto, Item>(CreatePropertyMapping<ShoppingItemDto, Item>(mapperConfiguration)));
        _propertyMappings.Add(new PropertyMapping<ShoppingListDto, ShoppingList>(CreatePropertyMapping<ShoppingListDto, ShoppingList>(mapperConfiguration)));
        _propertyMappings.Add(new PropertyMapping<StateDto, State>(CreatePropertyMapping<StateDto, State>(mapperConfiguration)));
        _propertyMappings.Add(new PropertyMapping<SubcategoryDto, Subcategory>(CreatePropertyMapping<SubcategoryDto, Subcategory>(mapperConfiguration)));
        _propertyMappings.Add(new PropertyMapping<TemporaryItemDto, Item>(CreatePropertyMapping<TemporaryItemDto, Item>(mapperConfiguration)));
        _propertyMappings.Add(new PropertyMapping<UserDto, User>(CreatePropertyMapping<UserDto, User>(mapperConfiguration)));
        _propertyMappings.Add(new PropertyMapping<GroupDto, Group>(CreatePropertyMapping<UserDto, User>(mapperConfiguration)));
    }

    public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSrc, TDst>()
    {
        var matching = _propertyMappings.OfType<PropertyMapping<TSrc, TDst>>();

        if (matching.Count() == 1)
        {
            return matching.First().MappingDictionary;
        }

        throw new Exception($"Cannot find exact property mapping instance for <{typeof(TSrc)},{typeof(TDst)}>");
    }

    private static Dictionary<string, PropertyMappingValue> CreatePropertyMapping<TSrc, TDst>(MapperConfiguration mapper)
        where TSrc : class
        where TDst : class
    {
        var result = new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase);
        foreach (var sourceProp in typeof(TSrc).GetProperties())
        {
            var destPropNames = GetDestinationPropertyFor<TSrc, TDst>(mapper, sourceProp.Name);
            result.Add(sourceProp.Name, new PropertyMappingValue(destPropNames));
        }

        return result;
    }

    private static IEnumerable<string> GetDestinationPropertyFor<TSrc, TDst>(MapperConfiguration mapper, string sourceProperty)
    {
        var map = mapper.FindTypeMapFor<TSrc, TDst>();

        if (map == null)
        {
            throw new Exception($"Cannot find exact property mapping instance for <{typeof(TSrc)},{typeof(TDst)}>");
        }

        var propertiesMap = map.PropertyMaps.Where(pm => pm.SourceMember.Name == sourceProperty);

        return propertiesMap.Select(p=>p.DestinationMember.Name);
    }
}
