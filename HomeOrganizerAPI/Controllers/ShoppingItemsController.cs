using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.Repositories;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Dto = HomeOrganizerAPI.Helpers.DTO.ShoppingItem;

namespace HomeOrganizerAPI.Controllers
{
    [ApiVersion("1.0")]
    [Authorize(Policy = "ApiReader")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ShoppingItemsController : BaseController<Item, ShoppingItem, Dto>
    {
        public ShoppingItemsController(HomeOrganizerContext context, IPropertyMappingService propertyMappingService) : base(new ShoppingItemsRepository(context, propertyMappingService))
        {
        }

        [HttpGet]
        public async Task<ActionResult<ResponseData<Dto>>> Get([FromQuery] ItemsResourceParameters resourceParameters)
        {
            return await BaseGet(resourceParameters);
        }
    }
}
