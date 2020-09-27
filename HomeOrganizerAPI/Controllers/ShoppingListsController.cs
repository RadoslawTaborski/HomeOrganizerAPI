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
    public class ShoppingListsController : BaseController<ShoppingList, ShoppingList, ShoppingListsController.Dto>
    {
        public ShoppingListsController(HomeOrganizerContext context) : base(new ShoppingListsRepository(context))
        {
        }

        protected override Dto FromObject(ShoppingList obj) => Dto.FromObject(obj);

        protected override ShoppingList ToObject(Dto obj) => Dto.ToObject(obj);

        [HttpGet]
        public async Task<ActionResult<ResponseData<Dto>>> Get([FromQuery] DefaultParameters resourceParameters)
        {
            return await BaseGet(resourceParameters);
        }

        public class Dto : Model
        {
            public string Name { get; set; }

            public static Dto FromObject(ShoppingList entity)
            {
                return new Dto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    CreateTime = entity.CreateTime,
                    UpdateTime = entity.UpdateTime,
                    DeleteTime = entity.DeleteTime,
                };
            }

            public static ShoppingList ToObject(Dto dto)
            {
                return new ShoppingList
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    CreateTime = dto.CreateTime,
                    UpdateTime = dto.UpdateTime,
                    DeleteTime = dto.DeleteTime
                };
            }
        }
    }
}