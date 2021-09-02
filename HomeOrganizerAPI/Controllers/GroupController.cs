using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.Repositories;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto = HomeOrganizerAPI.Helpers.DTO.Group;

namespace HomeOrganizerAPI.Controllers
{
    [ApiVersion("1.0")]
    [Authorize(Policy = "ApiReader")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class GroupController : BaseController<Group, Group, Dto>
    {
        public GroupController(HomeOrganizerContext context, IPropertyMappingService propertyMappingService) : base(new GroupRepository(context, propertyMappingService))
        {
        }

        [HttpGet]
        public async Task<ActionResult<ResponseData<Dto>>> Get([FromQuery] GroupsResourceParameters resourceParameters)
        {
            return await BaseGet(resourceParameters);
        }
    }
}
