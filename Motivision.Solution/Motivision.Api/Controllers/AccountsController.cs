using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Motivision.Api.DTOs;
using Motivision.API.Errors;
using Motivision.Core.Contracts.Services.Contracts;
using Motivision.Core.Identity.Entities;
using SnapShop.API.Dtos;
using System.Net;
using System.Security.Claims;

namespace Motivision.Api.Controllers
{
    public class AccountsController : BaseApiController
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("login")]  // /api/Accounts/login
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Unauthorized(new ApiResponse(401));
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));
            return Ok(new UserDto()
            {
                DisplayName = user.FullName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user, _userManager)
            });
        }

        [HttpPost("register")] // /api/Accounts/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (CheckEmailExists(registerDto.Email).Result.Value)
                return BadRequest(new ApiValidationErrorResponse() { Errors = new[] { "This Email Is Already In Use" } });
            var user = new AppUser()
            {
                FullName = registerDto.DisplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.Email.Split("@")[0]
            };
            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));
            return Ok(new UserDto()
            {
                DisplayName = user.FullName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user, _userManager)
            });
        }

        [Authorize]
        [HttpGet] //GET: /api/Accounts
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            return Ok(new UserDto()
            {
                DisplayName = user.FullName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user, _userManager)
            });
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<ActionResult> ChangePassword(ChangePasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            if (user == null) return Unauthorized(new ApiResponse(401));

            var result = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
            if (!result.Succeeded)
                return BadRequest(new ApiValidationErrorResponse
                {
                    Errors = result.Errors.Select(e => e.Description)
                });

            return Ok(new { Message = "Password updated successfully" });
        }

        [HttpGet("emailexists")] //GET: /api/Accounts/emailexists?email=xxx
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        [Authorize]
        [HttpPut("update-profile")]
        public async Task<ActionResult<UserDto>> UpdateProfile(UpdateProfileDto dto)
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            if (user == null) return Unauthorized(new ApiResponse(401));

            user.FullName = dto.FullName ?? user.FullName;
            user.PhoneNumber = dto.PhoneNumber ?? user.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return BadRequest(new ApiValidationErrorResponse
                {
                    Errors = result.Errors.Select(e => e.Description)
                });

            return Ok(new UserDto
            {
                DisplayName = user.FullName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user, _userManager)
            });
        }

        [Authorize]
        [HttpDelete("delete-account")]
        public async Task<ActionResult> DeleteAccount()
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            if (user == null) return Unauthorized();

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                return BadRequest(new ApiResponse(400));

            return Ok(new { Message = "Account deleted successfully." });
        }

    }
}