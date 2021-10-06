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

using Dto = HomeOrganizerAPI.Helpers.DTO.Expenses;

namespace HomeOrganizerAPI.Controllers
{
    [ApiVersion("1.0")]
    [Authorize(Policy = "ApiReader")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ExpensesController : BaseController<Expenses, Expenses, Dto, DefaultParameters>
    {
        private IPermissionChecker _checker;
        public ExpensesController(ExpensesRepository repo, IPermissionChecker checker) : base(repo)
        {
            _checker = checker;
        }

        protected override async Task<bool> HasAccessGet(ClaimsPrincipal user, DefaultParameters resourceParameters)
        {
            return await _checker.IsAtLeast(user, Guid.Parse(resourceParameters.GroupUuid).ToByteArray(), GroupRole.Member);
        }
        protected override async Task<bool> HasAccessGet(ClaimsPrincipal user, Expenses entity)
        {
            return await _checker.IsAtLeast(user, entity.GroupUuid, GroupRole.Member);
        }

        protected override async Task<bool> HasAccessPost(ClaimsPrincipal user, Dto entity)
        {
            return await _checker.IsAtLeast(user, entity.GroupUuid, GroupRole.Member);
        }

        protected override async Task<bool> HasAccessPut(ClaimsPrincipal user, Dto entity)
        {
            return await _checker.IsAtLeast(user, entity.GroupUuid, GroupRole.Member);
        }

        protected override async Task<bool> HasAccessDelete(ClaimsPrincipal user, Expenses entity)
        {
            return await _checker.IsAtLeast(user, entity.GroupUuid, GroupRole.Member);
        }
    }
}

