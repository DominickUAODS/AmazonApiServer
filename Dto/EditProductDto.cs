using AmazonApiServer.Attributes;
using System.Text.Json.Serialization;

namespace AmazonApiServer.Dto
{
    public class EditProductDto
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
        [JsonPropertyName("displays")]
        [FileExtensions("jpg")]
        public required IFormFileCollection Displays { get; set; }
    }
}
