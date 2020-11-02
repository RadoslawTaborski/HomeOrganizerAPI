
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.Repositories;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Dto = HomeOrganizerAPI.Helpers.DTO.ExpensesSettings;

namespace HomeOrganizerAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ExpensesSettingsController : BaseController<ExpensesSettings, ExpensesSettings, Dto>
    {
        public ExpensesSettingsController(HomeOrganizerContext context, IPropertyMappingService propertyMappingService) : base(new ExpensesSettingsRepository(context, propertyMappingService))
        {
        }

        [HttpGet]
        public async Task<ActionResult<ResponseData<Dto>>> Get([FromQuery] ExpensesSettingsResourceParameters resourceParameters)
        {
            return await BaseGet(resourceParameters);
        }
    }
}
