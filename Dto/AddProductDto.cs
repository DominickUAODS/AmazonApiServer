using AmazonApiServer.Models;
using System.Text.Json.Serialization;

namespace AmazonApiServer.Dto
{
    public class AddProductDto
    {
        [JsonPropertyName("code")]
        public required string Code { get; set; }
        [JsonPropertyName("category_id")]
        public Guid? CategoryId { get; set; }
        [JsonPropertyName("price")]
        public float Price { get; set; }
        [JsonPropertyName("discount")]
        public int? Discount { get; set; }
        [JsonPropertyName("number")]
        public int Number { get; set; }
    }
}
