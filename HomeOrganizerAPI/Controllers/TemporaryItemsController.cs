using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.Repositories;
using HomeOrganizerAPI.ResourceParameters;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Dto = HomeOrganizerAPI.Helpers.DTO.TemporaryItem;

namespace HomeOrganizerAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TemporaryItemsController : BaseController<Item, TemporaryItem, Dto>
    {
        public TemporaryItemsController(HomeOrganizerContext context) : base(new TemporaryItemsRepository(context))
        {
        }

        [HttpGet]
        public async Task<ActionResult<ResponseData<Dto>>> Get([FromQuery] TemporaryItemsResourceParameters resourceParameters)
        {
            return await BaseGet(resourceParameters);
        }
    }
}

