using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.Repositories;
using HomeOrganizerAPI.ResourceParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Dto = HomeOrganizerAPI.Helpers.DTO.Group;

namespace HomeOrganizerAPI.Controllers;

[ApiVersion("1.0")]
[Authorize(Policy = "ApiReader")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class GroupController : BaseController<Group, Group, Dto, GroupsResourceParameters>
{
    private IPermissionChecker _checker;
    public GroupController(GroupRepository repo, IPermissionChecker checker) : base(repo)
    {
        _checker = checker;
    }

    protected override async Task<bool> HasAccessGet(ClaimsPrincipal user, GroupsResourceParameters resourceParameters)
    {
        return await _checker.IsUserValid(user, Guid.Parse(resourceParameters.UserUuid).ToByteArray());
    }
    protected override async Task<bool> HasAccessGet(ClaimsPrincipal user, Group entity)
    {
        return await _checker.IsUserValid(user, entity);
    }

    protected override async Task<bool> HasAccessPost(ClaimsPrincipal user, Dto entity)
    {
        return await Task.FromResult(true);
    }

    protected override async Task<bool> HasAccessPut(ClaimsPrincipal user, Dto entity)
    {
        return await _checker.IsAtLeast(user, entity.Uuid, GroupRole.Member);
    }

    protected override async Task<bool> HasAccessDelete(ClaimsPrincipal user, Group entity)
    {
        return await _checker.IsAtLeast(user, entity.Uuid, GroupRole.Member);
    }
}
