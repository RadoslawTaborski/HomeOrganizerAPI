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
    public class StatesController : BaseController<State, State, StatesController.Dto>
    {
        public StatesController(HomeOrganizerContext context) : base(new StatesRepository(context))
        {
        }

        protected override Dto FromObject(State obj) => Dto.FromObject(obj);

        protected override State ToObject(Dto obj) => Dto.ToObject(obj);

        [HttpGet]
        public async Task<ActionResult<ResponseData<Dto>>> Get([FromQuery] DefaultParameters resourceParameters)
        {
            return await BaseGet(resourceParameters);
        }

        public class Dto : Model
        {
            public string Name { get; set; }

            public static Dto FromObject(State entity)
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

            public static State ToObject(Dto dto)
            {
                return new State
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