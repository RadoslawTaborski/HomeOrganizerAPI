using System.Security.Claims;
using System.Threading.Tasks;
using HomeOrganizerAPI.Models;

namespace HomeOrganizerAPI.Helpers;

public interface IPermissionChecker
{
    Task<bool> IsAtLeast(ClaimsPrincipal principal, byte[] groupUuid, GroupRole atLeast);
    Task<bool> IsUserValid(ClaimsPrincipal principal, byte[] userUuid);
    Task<bool> IsUserValid(ClaimsPrincipal user, Group entity);
}