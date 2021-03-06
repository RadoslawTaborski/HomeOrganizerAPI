﻿using System;
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
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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
            public string Description { get; set; }
            public bool Visible { get; set; }

            public static Dto FromObject(ShoppingList entity)
            {
                return new Dto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Description = entity.Description,
                    Visible = entity.Visible,
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
                    Description = dto.Description,
                    Visible = dto.Visible,
                    CreateTime = dto.CreateTime,
                    UpdateTime = dto.UpdateTime,
                    DeleteTime = dto.DeleteTime
                };
            }
        }
    }
}