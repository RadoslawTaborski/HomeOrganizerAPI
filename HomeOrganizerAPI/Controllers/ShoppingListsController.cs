using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.Repositories;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Dto = HomeOrganizerAPI.Helpers.DTO.ShoppingList;

namespace HomeOrganizerAPI.Controllers
{
    [ApiVersion("1.0")]
    [Authorize(Policy = "ApiReader")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ShoppingListsController : BaseController<ShoppingList, ShoppingList, Dto>
    {
        public ShoppingListsController(HomeOrganizerContext context, IPropertyMappingService propertyMappingService) : base(new ShoppingListsRepository(context, propertyMappingService))
        {
        }

        [HttpGet]
        public async Task<ActionResult<ResponseData<Dto>>> Get([FromQuery] ShoppingListResourceParameters resourceParameters)
        {
            return await BaseGet(resourceParameters);
        }
    }
}