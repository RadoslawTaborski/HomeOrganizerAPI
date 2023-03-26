using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.Repositories;
using HomeOrganizerAPI.ResourceParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

using Dto = HomeOrganizerAPI.Helpers.DTO.ExpenseDetails;

namespace HomeOrganizerAPI.Controllers;

[ApiVersion("1.0")]
[Authorize(Policy = "ApiReader")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class ExpenseDetailsController : BaseController<ExpenseDetails, ExpenseDetails, Dto, ExpenseDetialsResourceParameters>
{
    private IPermissionChecker _checker;
    public ExpenseDetailsController(ExpenseDetailsRepository repo, IPermissionChecker checker) : base(repo)
    {
        _checker = checker;
    }

    protected override async Task<bool> HasAccessGet(ClaimsPrincipal user, ExpenseDetialsResourceParameters resourceParameters)
    {
        return await _checker.IsAtLeast(user, Guid.Parse(resourceParameters.GroupUuid).ToByteArray(), GroupRole.Member);
    }
    protected override async Task<bool> HasAccessGet(ClaimsPrincipal user, ExpenseDetails entity)
    {
        return await _checker.IsAtLeast(user, entity.Expense.GroupUuid, GroupRole.Member);
    }

    protected override async Task<bool> HasAccessPost(ClaimsPrincipal user, Dto entity)
    {
        return await Task.FromResult(true);
    }

    protected override async Task<bool> HasAccessPut(ClaimsPrincipal user, Dto entity)
    {
        return await Task.FromResult(true);
    }

    protected override async Task<bool> HasAccessDelete(ClaimsPrincipal user, ExpenseDetails entity)
    {
        return await _checker.IsAtLeast(user, entity.Expense.GroupUuid, GroupRole.Member);
    }
}

