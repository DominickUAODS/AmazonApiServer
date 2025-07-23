using AmazonApiServer.Enums;
using System.Text.Json.Serialization;

namespace AmazonApiServer.Models
{
    public class Order
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("user")]
        public User? User { get; set; }
        [JsonPropertyName("order_status")]
        public OrderStatus Status { get; set; }
        [JsonPropertyName("ordered_on")]
        public DateOnly OrderedOn { get; set; }
        [JsonPropertyName("recipients_name")]
        public required string RecipientsName { get; set; }
        [JsonPropertyName("address")]
        public required string Address { get; set; }
        [JsonPropertyName("payment_type")]
        public PaymentType PaymentType { get; set; }
        [JsonPropertyName("order_items")]
        public List<OrderItem>? OrderItems { get; set; }
    }
}
