namespace AmazonApiServer.DTOs.DeliveryAddress
{
	public class DeliveryAddressCreateDto
	{
		public Guid Id { get; set; }
		public Guid CountryId { get; set; }
		public Guid? StateId { get; set; }
		public string City { get; set; } = string.Empty;
		public string Postcode { get; set; } = string.Empty;
	}
}
