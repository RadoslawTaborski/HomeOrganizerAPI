using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.Repositories;
using HomeOrganizerAPI.ResourceParameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HomeOrganizerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubcategoriesController : BaseController<Subcategory, Subcategory, SubcategoriesController.Dto>
    {
        public SubcategoriesController(HomeOrganizerContext context) : base(new SubcategoryRepository(context))
        {
        }

        protected override Dto FromObject(Subcategory obj) => Dto.FromObject(obj);

        protected override Subcategory ToObject(Dto obj) => Dto.ToObject(obj);

        [HttpGet]
        public async Task<ActionResult<ResponseData<Dto>>> Get([FromQuery] DefaultParameters resourceParameters)
        {
            return await BaseGet(resourceParameters);
        }

        public class Dto : Model
        {
            public string Name { get; set; }
            public int CategoryId { get; set; }

            public static Dto FromObject(Subcategory entity)
            {
                return new Dto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    CategoryId = entity.CategoryId,
                    CreateTime = entity.CreateTime,
                    UpdateTime = entity.UpdateTime,
                    DeleteTime = entity.DeleteTime,
                };
            }

            public static Subcategory ToObject(Dto dto)
            {
                return new Subcategory
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    CategoryId = dto.CategoryId,
                    CreateTime = dto.CreateTime,
                    UpdateTime = dto.UpdateTime,
                    DeleteTime = dto.DeleteTime
                };
            }
        }
    }
}