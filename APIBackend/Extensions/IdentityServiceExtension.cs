using System.Text;
using Core.Entities.Identity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace APIBackend.Extensions
{
    public static class IdentityServiceExtension
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services,IConfiguration config){
            services.AddDbContext<AppIdentityDBContext>(opt=>{
                opt.UseSqlite(config.GetConnectionString("IdentityConection"));
            });
            services.AddIdentityCore<AppUser>(opt=>{

            })
            .AddEntityFrameworkStores<AppIdentityDBContext>()
            .AddSignInManager<SignInManager<AppUser>>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters=new TokenValidationParameters{
                    ValidateIssuerSigningKey=true,
                    IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
                    ValidIssuer=config["Token:Issuer"],
                    ValidateIssuer=true,
                    ValidateAudience=false
                };
            });
            services.AddAuthorization();
            return services;
        }
    }
}