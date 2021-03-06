using BugTrace.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrace.Services.Interfaces
{
    public interface IBTRolesService
    {
        public Task<bool> AddUserToRoleAsync (BTUser user, string roleName );

        public Task<List<IdentityRole>> GetRolesAsync();

        public Task<string> GetRoleNameByIdAsync(string roleId);

        public Task<List<BTUser>> GetUsersInRoleAsync(string roleName, int companyId);

        public Task<BTUser> GetUsersNotinRoleAsync(string roleName, int companyId);

        public Task<IEnumerable<string>> GetUserRolesAsync(BTUser user);

        public Task<bool> IsUserInRoleAsync(BTUser user, string roleName);

        public Task<bool> RemoveUserFromRoleAsync(BTUser user, string roleName);

        public Task<bool> RemoveUserFromRolesAsync(BTUser user, IEnumerable<string> roles);
    }
}
