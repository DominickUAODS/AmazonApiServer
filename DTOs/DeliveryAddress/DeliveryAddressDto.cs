namespace AmazonApiServer.DTOs.DeliveryAddress
{
	public class DeliveryAddressDto
	{
		public Guid Id { get; set; }
		public Guid? CountryId { get; set; }
		public string CountryName { get; set; } = string.Empty;
		public string CountryStr { get; set; } = string.Empty;
		public Guid? StateId { get; set; }
		public string StateStr { get; set; } = string.Empty;
		public string StateName { get; set; } = string.Empty;
		public string City { get; set; } = string.Empty;
		public string Postcode { get; set; } = string.Empty;
	}
}