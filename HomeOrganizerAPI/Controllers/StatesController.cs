using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.Repositories;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Dto = HomeOrganizerAPI.Helpers.DTO.State;

namespace HomeOrganizerAPI.Controllers
{
    [ApiVersion("1.0")]
    [Authorize(Policy = "ApiReader")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class StatesController : BaseController<State, State, Dto>
    {
        public StatesController(HomeOrganizerContext context, IPropertyMappingService propertyMappingService) : base(new StatesRepository(context, propertyMappingService))
        {
        }

        [HttpGet]
        public async Task<ActionResult<ResponseData<Dto>>> Get([FromQuery] DefaultParameters resourceParameters)
        {
            return await BaseGet(resourceParameters);
        }
    }
}