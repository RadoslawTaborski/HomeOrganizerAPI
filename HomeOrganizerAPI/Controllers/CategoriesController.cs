using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.Repositories;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Dto = HomeOrganizerAPI.Helpers.DTO.Category;

namespace HomeOrganizerAPI.Controllers
{
    [ApiVersion("1.0")]
    [Authorize(Policy = "ApiReader")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController<Category, Category, Dto>
    {
        public CategoriesController(HomeOrganizerContext context, IPropertyMappingService propertyMappingService) : base(new CategoryRepository(context, propertyMappingService))
        {
        }

        [HttpGet]
        public async Task<ActionResult<ResponseData<Dto>>> Get([FromQuery] CategoryResourceParameters resourceParameters)
        {
            return await BaseGet(resourceParameters);
        }
    }
}

