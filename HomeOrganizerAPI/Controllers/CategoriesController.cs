using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.Repositories;
using HomeOrganizerAPI.ResourceParameters;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeOrganizerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController<Category, Category, CategoriesController.Dto>
    {
        public CategoriesController(HomeOrganizerContext context) : base(new CategoryRepository(context))
        {
        }

        protected override Dto FromObject(Category obj) => Dto.FromObject(obj);

        protected override Category ToObject(Dto obj) => Dto.ToObject(obj);

        [HttpGet]
        public async Task<ActionResult<ResponseData<Dto>>> Get([FromQuery] DefaultParameters resourceParameters)
        {
            return await BaseGet(resourceParameters);
        }

        public class Dto : Model
        {
            public string Name { get; set; }

            public static Dto FromObject(Category entity)
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

            public static Category ToObject(Dto dto)
            {
                return new Category
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

