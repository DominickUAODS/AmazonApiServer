namespace AmazonApiServer.DTOs.DeliveryAddress
{
	public class DeliveryAddressCreateDto
	{
		public Guid Id { get; set; }
		public Guid? CountryId { get; set; }
		public string CountryStr { get; set; } = string.Empty;
		public Guid? StateId { get; set; }
		public string StateStr { get; set; } = string.Empty;
		public string City { get; set; } = string.Empty;
		public string Postcode { get; set; } = string.Empty;
	}
}