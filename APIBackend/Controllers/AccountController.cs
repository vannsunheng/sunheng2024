using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using APIBackend.DTOS;
using APIBackend.Error;
using APIBackend.Extensions;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace APIBackend.Controllers
{
    public class AccountController : BaseAPIController
    {
        public UserManager<AppUser> UserManager { get; }
        public SignInManager<AppUser> SignInManager { get; }
        private readonly ITokenServices tokenServices;
        public IMapper Mapper { get; }
        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,
        ITokenServices _tokenServices,IMapper mapper)
        {
            this.Mapper = mapper;
            this.tokenServices = _tokenServices;
            this.SignInManager = signInManager;
            this.UserManager = userManager;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetCurrentUser(){

            var user=await UserManager.FindUserByEmailfromClaimPrinciple(User);
            
            return  new UserDTO{
                Email = user.Email,
                DisplayName=user.DisplayName,
                Token=tokenServices.CreateToken(user)
            };
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO){
            var user= await UserManager.FindByEmailAsync(loginDTO.Email);
            if(user==null) return Unauthorized(new APIResponce(401));
            var result=await SignInManager.CheckPasswordSignInAsync(user, loginDTO.Password,false);
            if(!result.Succeeded) return Unauthorized(new APIResponce(401));

            return new UserDTO{
                Email = user.Email,
                DisplayName=user.DisplayName,
                
                Token=tokenServices.CreateToken(user)
            };
        }
        [HttpGet("emailexist")]
        public async Task<ActionResult<bool>> CheckExistingEmail([FromQuery] string Email){
            return await UserManager.FindByEmailAsync(Email) != null;
        }
        [HttpGet("address")]
        public async Task<ActionResult<AddressDTO>> GetUserAddress(){
            var user=await UserManager.FindUserByClaimsPriciplewithAddress(User);
            return Mapper.Map<Address,AddressDTO>(user.Address);
        }
        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDTO>> UpdateUserAddress(AddressDTO address){

            var user=await UserManager.FindUserByClaimsPriciplewithAddress(HttpContext.User);

            user.Address=Mapper.Map<AddressDTO,Address>(address); 

            var result=await UserManager.UpdateAsync(user);

            if(result.Succeeded) return Ok(Mapper.Map<Address,AddressDTO>(user.Address));
            
            return BadRequest("Problem Updating for User");
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO){
            if (CheckExistingEmail(registerDTO.Email).Result.Value){
                return new BadRequestObjectResult(new APIValidationErrorResponse{Errors=new []
                {"Email address already in use"}});
            }
            var user= new AppUser{
                Email = registerDTO.Email,
                DisplayName=registerDTO.DisplayName,
                UserName=registerDTO.Email
            };
            var result=await UserManager.CreateAsync(user,registerDTO.Password);
            if(!result.Succeeded) return BadRequest(new APIResponce(400));
            return new UserDTO{
                DisplayName=registerDTO.DisplayName,
                Token=tokenServices.CreateToken(user),
                Email=registerDTO.Email
            };
        }   
    }
}