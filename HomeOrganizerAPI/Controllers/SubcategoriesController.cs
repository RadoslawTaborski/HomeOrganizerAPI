using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.Repositories;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Dto = HomeOrganizerAPI.Helpers.DTO.Subcategory;

namespace HomeOrganizerAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SubcategoriesController : BaseController<Subcategory, Subcategory, Dto>
    {
        public SubcategoriesController(HomeOrganizerContext context, IPropertyMappingService propertyMappingService) : base(new SubcategoryRepository(context, propertyMappingService))
        {
        }

        [HttpGet]
        public async Task<ActionResult<ResponseData<Dto>>> Get([FromQuery] SubcategoryResourceParameters resourceParameters)
        {
            return await BaseGet(resourceParameters);
        }
    }
}