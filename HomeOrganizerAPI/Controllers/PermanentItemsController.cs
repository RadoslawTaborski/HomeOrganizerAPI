using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.Repositories;
using HomeOrganizerAPI.ResourceParameters;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Dto = HomeOrganizerAPI.Helpers.DTO.PermanentItem;

namespace HomeOrganizerAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PermanentItemsController : BaseController<Item, PermanentItem, Dto>
    {
        public PermanentItemsController(HomeOrganizerContext context) : base(new PermanentItemsRepository(context))
        {
        }

        [HttpGet]
        public async Task<ActionResult<ResponseData<Dto>>> Get([FromQuery] PermanentItemsResourceParameters resourceParameters)
        {
            return await BaseGet(resourceParameters);
        }
    }
}
