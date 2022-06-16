using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.Repositories;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Dto = HomeOrganizerAPI.Helpers.DTO.ListCategory;
using HomeOrganizerAPI.Helpers;
using System.Security.Claims;
using System;

namespace HomeOrganizerAPI.Controllers
{
    [ApiVersion("1.0")]
    [Authorize(Policy = "ApiReader")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ListCategoriesController : BaseController<ListCategory, ListCategory, Dto, ListCategoryResourceParameters>
    {
        private IPermissionChecker _checker;
        public ListCategoriesController(ListCategoryRepository repo, IPermissionChecker checker) : base(repo)
        {
            _checker = checker;
        }

        protected override async Task<bool> HasAccessGet(ClaimsPrincipal user, ListCategoryResourceParameters resourceParameters)
        {
            return await _checker.IsAtLeast(user, Guid.Parse(resourceParameters.GroupUuid).ToByteArray(), GroupRole.Member);
        }
        
        protected override async Task<bool> HasAccessGet(ClaimsPrincipal user, ListCategory entity)
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

        protected override async Task<bool> HasAccessDelete(ClaimsPrincipal user, ListCategory entity)
        {
            return await _checker.IsAtLeast(user, entity.GroupUuid, GroupRole.Member);
        }
    }
}

