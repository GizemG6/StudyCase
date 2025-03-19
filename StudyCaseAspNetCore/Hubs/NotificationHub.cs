using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace StudyCaseAspNetCore.Hubs
{
	// SignalR Hub for handling real-time notifications
	public class NotificationHub : Hub
	{
		[Authorize]
		public async Task SendNotification(string message) // Method to send a notification message to all connected clients
		{
			// Sends the notification message to all connected clients using SignalR
			await Clients.All.SendAsync("ReceiveNotification", message);
		}
	}
}
