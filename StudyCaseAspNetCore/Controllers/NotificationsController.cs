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
	public class NotificationsController : ControllerBase
	{
		private readonly AppDbContext _context;
		private readonly IHubContext<NotificationHub> _hubContext;

		public NotificationsController(AppDbContext context, IHubContext<NotificationHub> hubContext)
		{
			_context = context;
			_hubContext = hubContext;
		}

		[HttpPost]
		public async Task<IActionResult> SendNotification([FromBody] Notification notification)
		{
			if (notification == null || string.IsNullOrEmpty(notification.Message))
				return BadRequest("Message cannot be empty!");

			// Veritabanına kaydet
			_context.Notifications.Add(notification);
			await _context.SaveChangesAsync();

			// SignalR ile istemcilere bildirimi gönder
			await _hubContext.Clients.All.SendAsync("ReceiveNotification", notification.Message);

			return Ok(new { message = "Notification sent successfully!" });
		}

		[HttpGet]
		public IActionResult GetNotifications()
		{
			var notifications = _context.Notifications.OrderByDescending(n => n.SentDate).ToList();
			return Ok(notifications);
		}
	}
}
