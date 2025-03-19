using System.ComponentModel.DataAnnotations;

namespace StudyCaseAspNetCore.Models
{
	public class Notification
	{
		public int Id { get; set; }

		[Required]
		public string Message { get; set; }

		public DateTime SentDate { get; set; } = DateTime.UtcNow;
	}
}
