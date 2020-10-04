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
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : BaseController<User, User, UsersController.Dto>
    {
        public UsersController(HomeOrganizerContext context) : base(new UsersRepository(context))
        {
        }

        protected override Dto FromObject(User obj) => Dto.FromObject(obj);

        protected override User ToObject(Dto obj) => Dto.ToObject(obj);

        [HttpGet]
        public async Task<ActionResult<ResponseData<Dto>>> Get([FromQuery] DefaultParameters resourceParameters)
        {
            return await BaseGet(resourceParameters);
        }

        public class Dto : Model
        {
            public string Username { get; set; }
            public string Email { get; set; }

            public static Dto FromObject(User entity)
            {
                return new Dto
                {
                    Id = entity.Id,
                    Username = entity.Username,
                    Email = entity.Email,
                    CreateTime = entity.CreateTime,
                    UpdateTime = entity.UpdateTime,
                    DeleteTime = entity.DeleteTime,
                };
            }

            public static User ToObject(Dto dto)
            {
                return new User
                {
                    Id = dto.Id,
                    Username = dto.Username,
                    Email = dto.Email,
                    CreateTime = dto.CreateTime,
                    UpdateTime = dto.UpdateTime,
                    DeleteTime = dto.DeleteTime
                };
            }
        }
    }
}