using System.Text.Json.Serialization;

namespace AmazonApiServer.Filters
{
    public class ProductsFilter
    {
        [JsonPropertyName("category")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Guid? CategoryId { get; set; }
        // todo more filter options
    }
}
