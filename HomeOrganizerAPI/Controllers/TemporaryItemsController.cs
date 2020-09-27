using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.Repositories;
using HomeOrganizerAPI.ResourceParameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeOrganizerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemporaryItemsController : BaseController<Item, TemporaryItem, TemporaryItem>
    {
        public TemporaryItemsController(HomeOrganizerContext context) : base(new TemporaryItemsRepository(context))
        {
        }

        [HttpGet]
        public async Task<ActionResult<ResponseData<TemporaryItem>>> Get([FromQuery] TemporaryItemsResourceParameters resourceParameters)
        {
            return await BaseGet(resourceParameters);
        }

        protected override TemporaryItem FromObject(Item obj)
        {
            return new TemporaryItem
            {
                Id = obj.Id,
                Name = obj.Name,
                ShoppingListId = obj.ShoppingListId,
                Quantity = obj.Quantity,
                CategoryId = obj.CategoryId,
                Bought = obj.Bought,
                CreateTime = obj.CreateTime,
                UpdateTime = obj.UpdateTime,
                DeleteTime = obj.DeleteTime,
            };
        }

        protected override Item ToObject(TemporaryItem obj)
        {
            return new Item
            {
                Id = obj.Id,
                Name = obj.Name,
                ShoppingListId = obj.ShoppingListId,
                Quantity = obj.Quantity,
                CategoryId = obj.CategoryId,
                Bought = obj.Bought,
                CreateTime = obj.CreateTime,
                UpdateTime = obj.UpdateTime,
                DeleteTime = obj.DeleteTime
            };
        }
    }
}

