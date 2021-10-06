using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.Repositories;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Dto = HomeOrganizerAPI.Helpers.DTO.Category;
using HomeOrganizerAPI.Helpers;
using System.Security.Claims;
using System;

namespace HomeOrganizerAPI.Controllers
{
    [ApiVersion("1.0")]
    [Authorize(Policy = "ApiReader")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController<Category, Category, Dto, CategoryResourceParameters>
    {
        private IPermissionChecker _checker;
        public CategoriesController(CategoryRepository repo, IPermissionChecker checker) : base(repo)
        {
            _checker = checker;
        }

        protected override async Task<bool> HasAccessGet(ClaimsPrincipal user, CategoryResourceParameters resourceParameters)
        {
            return await _checker.IsAtLeast(user, Guid.Parse(resourceParameters.GroupUuid).ToByteArray(), GroupRole.Member);
        }
        
        protected override async Task<bool> HasAccessGet(ClaimsPrincipal user, Category entity)
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

        protected override async Task<bool> HasAccessDelete(ClaimsPrincipal user, Category entity)
        {
            return await _checker.IsAtLeast(user, entity.GroupUuid, GroupRole.Member);
        }
    }
}

