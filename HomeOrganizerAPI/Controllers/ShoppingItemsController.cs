using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.Repositories;
using HomeOrganizerAPI.ResourceParameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeOrganizerAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ShoppingItemsController : BaseController<Item, ShoppingItem, ShoppingItem>
    {
        public ShoppingItemsController(HomeOrganizerContext context) : base(new ShoppingItemsRepository(context))
        {
        }

        [HttpGet]
        public async Task<ActionResult<ResponseData<ShoppingItem>>> Get([FromQuery] ItemsResourceParameters resourceParameters)
        {
            return await BaseGet(resourceParameters);
        }

        protected override ShoppingItem FromObject(Item obj)
        {
            return new ShoppingItem
            {
                Id = obj.Id,
                Name = obj.Name,
                StateId = obj.StateId,
                CategoryId = obj.CategoryId,
                ShoppingListId = obj.ShoppingListId,
                Quantity = obj.Quantity,
                Bought = obj.Bought,
                CreateTime = obj.CreateTime,
                UpdateTime = obj.UpdateTime,
                DeleteTime = obj.DeleteTime    
            };
        }

        protected override Item ToObject(ShoppingItem obj)
        {
            return new Item
            {
                Id = obj.Id,
                Name = obj.Name,
                StateId = obj.StateId,
                CategoryId = obj.CategoryId,
                ShoppingListId = obj.ShoppingListId,
                Quantity = obj.Quantity,
                Bought = obj.Bought,
                CreateTime = obj.CreateTime,
                UpdateTime = obj.UpdateTime,
                DeleteTime = obj.DeleteTime
            };
        }
    }
}
