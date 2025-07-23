using System.Text.Json.Serialization;

namespace AmazonApiServer.Models
{
    public class OrderItem
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("order")]
        public Order? Order { get; set; }
        [JsonPropertyName("product")]
        public Product? Product { get; set; }
        [JsonPropertyName("number")]
        public int Number { get; set; }
    }
}
