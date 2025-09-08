namespace AmazonApiServer.DTOs.DeliveryAddress
{
	public class DeliveryAddressDto
	{
		public Guid Id { get; set; }
		public Guid CountryId { get; set; }
		public string CountryName { get; set; } = string.Empty;
		public Guid? StateId { get; set; }
		public string? StateName { get; set; }
		public string City { get; set; } = string.Empty;
		public string Postcode { get; set; } = string.Empty;
	}
}
