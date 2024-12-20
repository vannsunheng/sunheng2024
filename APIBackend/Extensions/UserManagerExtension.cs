using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Permissions;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace APIBackend.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task<AppUser> FindUserByClaimsPriciplewithAddress(this UserManager<AppUser> userManager,
        ClaimsPrincipal principal){
            var email=principal.FindFirstValue(ClaimTypes.Email);
            return await userManager.Users.Include(x=>x.Address).SingleOrDefaultAsync(x=>x.Email==email);
        } 
        public static async Task<AppUser> FindUserByEmailfromClaimPrinciple(this UserManager<AppUser> userManager,
        ClaimsPrincipal user){
            return await userManager.Users.SingleOrDefaultAsync(x=>x.Email==user.FindFirstValue(ClaimTypes.Email));
        }
    }
}