using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.Repositories;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Dto = HomeOrganizerAPI.Helpers.DTO.User;

namespace HomeOrganizerAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : BaseController<User, User, Dto, Dto>
    {
        public UsersController(HomeOrganizerContext context, IPropertyMappingService propertyMappingService) : base(new UsersRepository(context, propertyMappingService))
        {
        }

        [HttpGet]
        public async Task<ActionResult<ResponseData<Dto>>> Get([FromQuery] UsersResourceParameters resourceParameters)
        {
            return await BaseGet(resourceParameters);
        }

        [HttpGet("{username}/{password}")]
        public async Task<ActionResult<Dto>> Get(string username, string password)
        {
            var entity = await (_repo as UsersRepository).Get(username, password);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(_mapperTOut.ToDto(entity));
        }
    }
}