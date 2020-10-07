using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.Repositories;
using HomeOrganizerAPI.ResourceParameters;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Dto = HomeOrganizerAPI.Helpers.DTO.ShoppingItem;

namespace HomeOrganizerAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ShoppingItemsController : BaseController<Item, ShoppingItem, Dto>
    {
        public ShoppingItemsController(HomeOrganizerContext context) : base(new ShoppingItemsRepository(context))
        {
        }

        [HttpGet]
        public async Task<ActionResult<ResponseData<Dto>>> Get([FromQuery] ItemsResourceParameters resourceParameters)
        {
            return await BaseGet(resourceParameters);
        }
    }
}
