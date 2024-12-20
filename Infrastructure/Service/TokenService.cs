using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Entities.Identity;
using Core.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Service
{
    public class TokenService:ITokenServices
    {
        private readonly IConfiguration configuration;
        private readonly SymmetricSecurityKey securityKey;
        public TokenService(IConfiguration config)
        {
            this.configuration = config;
            
            securityKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"]));

        }
        public string CreateToken(AppUser appUser)
        {
            var claims=new  List<Claim>{
                new Claim(ClaimTypes.Email, appUser.Email),
                new Claim(ClaimTypes.GivenName,appUser.DisplayName) 
            };
            Console.WriteLine(configuration["Token:Key"]);
            var Creds=new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha512Signature);
            var tokendescriptor=new SecurityTokenDescriptor{
                Subject=new ClaimsIdentity(claims),
                Expires=DateTime.Now.AddDays(7),
                SigningCredentials=Creds,
                Issuer=configuration["Token:Issuer"],
            };
            var tokenhandler=new JwtSecurityTokenHandler();
            var token=tokenhandler.CreateToken(tokendescriptor);
            return tokenhandler.WriteToken(token);
        }
    }
}