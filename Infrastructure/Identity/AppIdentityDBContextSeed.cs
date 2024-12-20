
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDBContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager){
            if(!userManager.Users.Any()){
                var user=new AppUser{
                    DisplayName="Sunhengjr",
                    Email="sunhengjr@gmail.com",
                    UserName="sunhengjr@gmail.com",
                    Address=new Address{
                        
                        FristName="Sunheng",
                        LastName="Jr",
                        Street="85 KM,3 National Road",
                        City="Nheang Nhang",
                        State="Takeo",
                        ZipCode="120284",

                    }
                };
                await userManager.CreateAsync(user,"Pa$$W0rd@2025");    
            }
        }
    }
}