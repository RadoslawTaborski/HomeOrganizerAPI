using AutoMapper;
using HomeOrganizerAPI.Models;
using CategoryDto = HomeOrganizerAPI.Helpers.DTO.Category;
using ExpensesDto = HomeOrganizerAPI.Helpers.DTO.Expenses;
using ItemDto = HomeOrganizerAPI.Helpers.DTO.Item;
using PermanentItemDto = HomeOrganizerAPI.Helpers.DTO.PermanentItem;
using SaldoDto = HomeOrganizerAPI.Helpers.DTO.Saldo;
using ShoppingItemDto = HomeOrganizerAPI.Helpers.DTO.ShoppingItem;
using ShoppingListDto = HomeOrganizerAPI.Helpers.DTO.ShoppingList;
using StateDto = HomeOrganizerAPI.Helpers.DTO.State;
using SubcategoryDto = HomeOrganizerAPI.Helpers.DTO.Subcategory;
using TemporaryItemDto = HomeOrganizerAPI.Helpers.DTO.TemporaryItem;
using UserDto = HomeOrganizerAPI.Helpers.DTO.User;

namespace HomeOrganizerAPI.Helpers
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration MapperConfiguration { get; private set; }

        public static void RegisterMappings()
        {
            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryDto>().ReverseMap();
                cfg.CreateMap<Item, ShoppingItemDto>().ReverseMap();
                cfg.CreateMap<User, UserDto>().ReverseMap();
                cfg.CreateMap<Item, TemporaryItemDto>().ReverseMap();
                cfg.CreateMap<Subcategory, SubcategoryDto>().ReverseMap();
                cfg.CreateMap<State, StateDto>().ReverseMap();
                cfg.CreateMap<ShoppingList, ShoppingListDto>().ReverseMap();
                cfg.CreateMap<Saldo, SaldoDto>().ReverseMap();
                cfg.CreateMap<Item, PermanentItemDto>().ReverseMap();
                cfg.CreateMap<Item, ItemDto>().ReverseMap();
                cfg.CreateMap<Expenses, ExpensesDto>().ReverseMap();
            });
        }
    }
}
