using System.ComponentModel.DataAnnotations;

namespace AmazonApiServer.Enums
{
    public enum OrderStatus
    {
        [Display(Name = "Received")]
        RECEIVED,
        [Display(Name = "Ready to pickup")]
        READY_TO_PICKUP,
        [Display(Name = "Shipped")]
        SHIPPED,
        [Display(Name = "Ordered")]
        ORDERED,
        [Display(Name = "Cancelled")]
        CANCELLED
    }
}
