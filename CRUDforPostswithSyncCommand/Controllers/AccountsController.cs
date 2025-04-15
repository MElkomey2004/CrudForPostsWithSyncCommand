using AutoMapper;
using CRUDforPostswithSyncCommand.DTOs;
using CRUDforPostswithSyncCommand.Entities;
using CRUDforPostswithSyncCommand.JwtFeatures;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRUDforPostswithSyncCommand.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountsController : ControllerBase
	{
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<Role> _roleManager;
		private readonly IMapper _mapper;
		private readonly JWTHandler _jwtHandler;

		public AccountsController(
			UserManager<User> userManager,
			RoleManager<Role> roleManager,
			IMapper mapper,
			JWTHandler jwtHandler)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_mapper = mapper;
			_jwtHandler = jwtHandler;
		}

		[HttpPost("register")]
		public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
		{
			if (userForRegistration == null)
				return BadRequest("Invalid registration request.");

			var user = _mapper.Map<User>(userForRegistration);

			// Ensure roles exist in the system
			string[] roles = new[] { "Admin", "Reviewer" };
			foreach (var role in roles)
			{
				if (!await _roleManager.RoleExistsAsync(role))
				{
					await _roleManager.CreateAsync(new Role { Name = role });
				}
			}

			var result = await _userManager.CreateAsync(user, userForRegistration.Password);

			if (!result.Succeeded)
			{
				var errors = result.Errors.Select(e => e.Description);
				return BadRequest(new RegistrationResponseDto { Errors = errors });
			}

			await _userManager.AddToRoleAsync(user, "Admin");

			return StatusCode(201, new { Message = "User created and assigned to Admin role." });
		}

		[HttpPost("authenticate")]
		public async Task<IActionResult> Authenticate([FromBody] UserFroAuthenticationDto userForAuthentication)
		{
			if (userForAuthentication == null)
				return BadRequest("Missing authentication data.");

			var user = await _userManager.FindByEmailAsync(userForAuthentication.Email!);

			if (user == null)
				return BadRequest("Invalid request.");

		
			if (!await _userManager.CheckPasswordAsync(user, userForAuthentication.Password!))
				return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid credentials." });

			var roles = await _userManager.GetRolesAsync(user);

			var token = _jwtHandler.createToken(user, roles);

			return Ok(new AuthResponseDto { IsSuccessfull = true, Token = token });
		}
	}
}
