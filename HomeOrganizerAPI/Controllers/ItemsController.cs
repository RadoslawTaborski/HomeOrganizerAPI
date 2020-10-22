using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.Repositories;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Dto = HomeOrganizerAPI.Helpers.DTO.Item;

namespace HomeOrganizerAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ItemsController : BaseController<Item, Item, Dto>
    {
        public ItemsController(HomeOrganizerContext context, IPropertyMappingService propertyMappingService) : base(new ItemRepository(context, propertyMappingService: propertyMappingService))
        {
        }

        [HttpGet]
        public async Task<ActionResult<ResponseData<Dto>>> Get([FromQuery] ItemsResourceParameters resourceParameters)
        {
            return await BaseGet(resourceParameters);
        }
    } 
}
