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
    public class PermanentItemsController : BaseController<Item, PermanentItem, PermanentItem>
    {
        public PermanentItemsController(HomeOrganizerContext context) : base(new PermanentItemsRepository(context))
        {
        }

        [HttpGet]
        public async Task<ActionResult<ResponseData<PermanentItem>>> Get([FromQuery] PermanentItemsResourceParameters resourceParameters)
        {
            return await BaseGet(resourceParameters);
        }

        protected override PermanentItem FromObject(Item obj)
        {
            return new PermanentItem
            {
                Id = obj.Id,
                Name = obj.Name,
                StateId = obj.StateId,
                CategoryId = obj.CategoryId,
                CreateTime = obj.CreateTime,
                UpdateTime = obj.UpdateTime,
                DeleteTime = obj.DeleteTime,
            };
        }

        protected override Item ToObject(PermanentItem obj)
        {
            return new Item
            {
                Id = obj.Id,
                Name = obj.Name,
                StateId = obj.StateId,
                CategoryId = obj.CategoryId,
                CreateTime = obj.CreateTime,
                UpdateTime = obj.UpdateTime,
                DeleteTime = obj.DeleteTime
            };
        }
    }
}
