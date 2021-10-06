using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.Repositories;

namespace HomeOrganizerAPI.Helpers
{
    public class PermissionChecker : IPermissionChecker
    {
        private GroupRepository _groupRepository;
        private UsersRepository _userRepository;
        private Dictionary<ClaimsPrincipal, GroupRole> _cache;

        public PermissionChecker(GroupRepository groupRepository, UsersRepository usersRepository)
        {
            _groupRepository = groupRepository;
            _userRepository = usersRepository;
            _cache = new Dictionary<ClaimsPrincipal, GroupRole>();
        }

        public async Task<bool> IsUserValid(ClaimsPrincipal principal, byte[] userUuid){
            var externalUuid = getSub(principal);
            var user = await _userRepository.Get(externalUuid);
            return user.Uuid.SequenceEqual(userUuid);
        }

        public async Task<bool> IsUserValid(ClaimsPrincipal principal, Group entity){
            var externalUuid = getSub(principal);
            var user = await _userRepository.Get(externalUuid);
            return entity.UserGroups.Any(x=>x.User.Uuid.SequenceEqual(user.Uuid));
        }

        public async Task<bool> IsAtLeast(ClaimsPrincipal principal, byte[] groupUuid, GroupRole atLeast){
            var role = await this.Check(principal, groupUuid);
            if((role >= atLeast)){
                return true;
            }
            return false;
        }
                                        
        private async Task<GroupRole> Check(ClaimsPrincipal principal, byte[] groupUuid)
        {
            if (_cache.TryGetValue(principal, out var result))
            {
                return result;
            }
            else
            {
                var group = await _groupRepository.Get(groupUuid);
                var externalUuid = getSub(principal);
                var user = await _userRepository.Get(externalUuid);
                if (group.UserGroups.Any(x => x.User.Uuid.SequenceEqual(user.Uuid) && x.Owner == true))
                {
                    _cache.Add(principal, GroupRole.Owner);
                    return GroupRole.Owner;
                }
                else if (group.UserGroups.Any(x => x.User.Uuid.SequenceEqual(user.Uuid)))
                {
                    _cache.Add(principal, GroupRole.Member);
                    return GroupRole.Member;
                }
            }
            return GroupRole.None;
        }

        private string getSub(ClaimsPrincipal principal)
        {
            return principal.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
        }
    }
}