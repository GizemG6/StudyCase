using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StudyCaseAspNetCore.Context;
using StudyCaseAspNetCore.Hubs;
using StudyCaseAspNetCore.Models;

namespace StudyCaseAspNetCore.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class NotificationsController : ControllerBase
	{
		// Injecting the DbContext and SignalR Hub context
		private readonly AppDbContext _context;
		private readonly IHubContext<NotificationHub> _hubContext;

		// Constructor to initialize the context and HubContext
		public NotificationsController(AppDbContext context, IHubContext<NotificationHub> hubContext)
		{
			_context = context;
			_hubContext = hubContext;
		}

		[HttpPost]
		public async Task<IActionResult> SendNotification([FromBody] Notification notification)
		{
			// Validate notification message
			if (notification == null || string.IsNullOrEmpty(notification.Message))
				return BadRequest("Message cannot be empty!");

			// Save the notification to the database
			_context.Notifications.Add(notification);
			await _context.SaveChangesAsync();

			// Send the notification message to all connected clients using SignalR
			await _hubContext.Clients.All.SendAsync("ReceiveNotification", notification.Message);

			// Return success response
			return Ok(new { message = "Notification sent successfully!" });
		}

		[HttpGet]
		public IActionResult GetNotifications()
		{
			// Retrieve and order notifications by the sent date (most recent first)
			var notifications = _context.Notifications.OrderByDescending(n => n.SentDate).ToList();
			return Ok(notifications);
		}
	}
}
