
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudyCaseAspNetCore.Context;
using StudyCaseAspNetCore.Hubs;
using System.Text;

namespace StudyCaseAspNetCore
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add DbContext for Entity Framework with SQL Server connection
			builder.Services.AddDbContext<AppDbContext>(options =>
	        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

			// Add authentication service using JWT Bearer token
			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						// Enable validation for the issuer, audience, and token's expiration
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						// Retrieve issuer, audience, and signing key from configuration
						ValidIssuer = builder.Configuration["Jwt:Issuer"],
						ValidAudience = builder.Configuration["Jwt:Audience"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
					};
				});

			// Add authorization services
			builder.Services.AddAuthorization();

			// Add SignalR service for real-time communication
			builder.Services.AddSignalR();

			builder.Services.AddControllers();

			var app = builder.Build();

			// Serve static files from the wwwroot directory
			app.UseStaticFiles();

			app.UseHttpsRedirection();

			// Enable authentication middleware to authenticate users based on JWT token
			app.UseAuthentication();

			app.UseAuthorization();

			app.MapControllers();

			// Map SignalR hub for real-time communication
			app.MapHub<NotificationHub>("/notificationHub");

			app.Run();
		}
	}
}
