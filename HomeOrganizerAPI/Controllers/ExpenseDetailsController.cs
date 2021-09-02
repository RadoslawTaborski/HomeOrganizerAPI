using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.Repositories;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using Dto = HomeOrganizerAPI.Helpers.DTO.ExpenseDetails;

namespace HomeOrganizerAPI.Controllers
{
    [ApiVersion("1.0")]
    [Authorize(Policy = "ApiReader")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ExpenseDetailsController : BaseController<ExpenseDetails, ExpenseDetails, Dto>
    {
        public ExpenseDetailsController(HomeOrganizerContext context, IPropertyMappingService propertyMappingService) : base(new ExpenseDetailsRepository(context, propertyMappingService))
        {
        }

        [HttpGet]
        public async Task<ActionResult<ResponseData<Dto>>> Get([FromQuery] ExpenseDetialsResourceParameters resourceParameters)
        {
            return await BaseGet(resourceParameters);
        }
    }
}

