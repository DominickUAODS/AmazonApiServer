using System.ComponentModel.DataAnnotations;

namespace AmazonApiServer.DTOs.Review
{
	public class ReviewUpdateDto : ReviewCreateDto
	{
		[Required]
		public Guid Id { get; set; }
	}
}
