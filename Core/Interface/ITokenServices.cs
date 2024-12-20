using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;

namespace Core.Interface
{
    public interface ITokenServices
    {
        string CreateToken(AppUser appUser);
    }
}