using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.Repositories;
using HomeOrganizerAPI.ResourceParameters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace HomeOrganizerAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ItemsController : BaseController<Item, Item, ItemsController.Dto>
    {
        public ItemsController(HomeOrganizerContext context) : base(new ItemRepository(context))
        {
        }

        protected override Dto FromObject(Item obj) => Dto.FromObject(obj);

        protected override Item ToObject(Dto obj) => Dto.ToObject(obj);

        [HttpGet]
        public async Task<ActionResult<ResponseData<Dto>>> Get([FromQuery] ItemsResourceParameters resourceParameters)
        {
            return await BaseGet(resourceParameters);
        }

        public class Dto : Model
        {
            public string Name { get; set; }
            public int? ShoppingListId { get; set; }
            public int? StateId { get; set; }
            public string Quantity { get; set; }
            public int CategoryId { get; set; }
            public DateTimeOffset? Bought { get; set; }

            public static Dto FromObject(Item entity)
            {
                return new Dto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    ShoppingListId = entity.ShoppingListId,
                    StateId = entity.StateId,
                    Quantity = entity.Quantity,
                    CategoryId = entity.CategoryId,
                    Bought = entity.Bought,
                    CreateTime = entity.CreateTime,
                    UpdateTime = entity.UpdateTime,
                    DeleteTime = entity.DeleteTime,
                };
            }

            public static Item ToObject(Dto dto)
            {
                return new Item
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    ShoppingListId = dto.ShoppingListId,
                    StateId = dto.StateId,
                    Quantity = dto.Quantity,
                    CategoryId = dto.CategoryId,
                    Bought = dto.Bought,
                    CreateTime = dto.CreateTime,
                    UpdateTime = dto.UpdateTime,
                    DeleteTime = dto.DeleteTime
                };
            }
        }
    }
}
