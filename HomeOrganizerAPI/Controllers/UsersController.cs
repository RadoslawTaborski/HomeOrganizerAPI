using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.Repositories;
using HomeOrganizerAPI.ResourceParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Dto = HomeOrganizerAPI.Helpers.DTO.User;

namespace HomeOrganizerAPI.Controllers;

[ApiVersion("1.0")]
[Authorize(Policy = "ApiReader")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class UsersController : BaseController<User, User, Dto, UsersResourceParameters>
{
    private IPermissionChecker _checker;
    public UsersController(UsersRepository repo, IPermissionChecker checker) : base(repo)
    {
        _checker = checker;
    }

    protected override async Task<bool> HasAccessGet(ClaimsPrincipal user, UsersResourceParameters resourceParameters)
    {
        return await _checker.IsAtLeast(user, Guid.Parse(resourceParameters.GroupUuid).ToByteArray(), GroupRole.Member);
    }

    [HttpGet("{id}")]
    public override async Task<ActionResult<Dto>> BaseGet(string id)
    {
        var entity = await (_repo as UsersRepository).Get(id);
        if (entity == null)
        {
            return NotFound();
        }

        return Ok(_mapper.ToDto(entity));
    }
}