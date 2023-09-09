using System.ComponentModel.DataAnnotations;

namespace Leaderboard_System__Dell_.Models
{
	public class Points
	{
		[Required]
		public int ID { get; set; }

		[Required]
		[StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
		public string Name { get; set; }

		[Required]
		public int Score { get; set; }
	}
}
