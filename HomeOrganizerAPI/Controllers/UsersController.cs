using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.Repositories;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Dto = HomeOrganizerAPI.Helpers.DTO.User;

namespace HomeOrganizerAPI.Controllers
{
    [ApiVersion("1.0")]
    [Authorize(Policy = "ApiReader")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : BaseController<User, User, Dto>
    {
        public UsersController(HomeOrganizerContext context, IPropertyMappingService propertyMappingService) : base(new UsersRepository(context, propertyMappingService))
        {
        }

        [HttpGet]
        public async Task<ActionResult<ResponseData<Dto>>> Get([FromQuery] UsersResourceParameters resourceParameters)
        {
            return await BaseGet(resourceParameters);
        }

        [HttpGet("{id}")]
        public override async Task<ActionResult<Dto>> BaseGet(string id)
        {
            var entity = await (_repo as UsersRepository).Get(id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(_mapper.ToDto(entity));
        }
    }
}