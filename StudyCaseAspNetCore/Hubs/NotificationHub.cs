using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace StudyCaseAspNetCore.Hubs
{
	public class NotificationHub : Hub
	{
		[Authorize]
		public async Task SendNotification(string message)
		{
			await Clients.All.SendAsync("ReceiveNotification", message);
		}
	}
}
