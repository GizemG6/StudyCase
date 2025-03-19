using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudyCaseAspNetCore.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudyCaseAspNetCore.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IConfiguration _config;

		// Constructor that injects the configuration service to access JWT settings from the appsettings.json
		public AuthController(IConfiguration config)
		{
			_config = config;
		}

		[HttpPost("login")]
		public IActionResult Login([FromBody]LoginRequestDto request)
		{
			// Verify user credentials (In a real-world application, you'd query the database)
			if (request.Email == "admin@example.com" && request.Password == "123456")
			{
				// If credentials are valid, generate a JWT token
				var token = GenerateJwtToken(request.Email);
				return Ok(new { token });
			}
			// Return Unauthorized if email or password is incorrect
			return Unauthorized("Invalid email or password!");
		}

		// Private method to generate a JWT token
		private string GenerateJwtToken(string email)
		{
			// Create a security key using the secret key stored in the configuration
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

			// Set up the signing credentials with the security key and algorithm
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			// Define the claims for the JWT token (e.g., user email and role)
			var claims = new[]
			{
				new Claim(ClaimTypes.Name, email),
				new Claim(ClaimTypes.Role, "Admin")
		    };

			// Create the JWT token with issuer, audience, claims, expiration, and signing credentials
			var token = new JwtSecurityToken(
				_config["Jwt:Issuer"],
				_config["Jwt:Audience"],
				claims,
				expires: DateTime.UtcNow.AddHours(1),
				signingCredentials: credentials
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
