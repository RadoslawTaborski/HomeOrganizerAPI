using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.Repositories;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Dto = HomeOrganizerAPI.Helpers.DTO.State;

namespace HomeOrganizerAPI.Controllers
{
    [ApiVersion("1.0")]
    [Authorize(Policy = "ApiReader")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class StatesController : BaseController<State, State, Dto, DefaultParameters>
    {
        private IPermissionChecker _checker;
        public StatesController(StatesRepository repo, IPermissionChecker checker) : base(repo)
        {
            _checker = checker;
        }

        protected override async Task<bool> HasAccessGet(ClaimsPrincipal user, DefaultParameters resourceParameters)
        {
            return await Task.FromResult(true);
        }
        
        protected override async Task<bool> HasAccessGet(ClaimsPrincipal user, State entity)
        {
            return await Task.FromResult(true);
        }
    }
}