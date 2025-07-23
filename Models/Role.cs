using System.Text.Json.Serialization;

namespace AmazonApiServer.Models
{
    public class Role
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public required string Name { get; set; }
        [JsonPropertyName("users")]
        public List<User>? Users { get; set; }
    }
}
