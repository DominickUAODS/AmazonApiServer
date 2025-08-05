namespace AmazonApiServer.DTOs.Review
{
	public class ReviewDto
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public Guid ProductId { get; set; }
		public int Stars { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Content { get; set; } = string.Empty;
		public DateTime Published { get; set; }
	}
}
