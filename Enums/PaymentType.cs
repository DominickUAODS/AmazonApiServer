using System.ComponentModel.DataAnnotations;

namespace AmazonApiServer.Enums
{
    public enum PaymentType
    {
        [Display(Name = "Credit card", Description = "Save your card in your Perry account to pay faster and more conveniently. After saving your card, you don’t have to login to the bank, enter codes or enter data for subsequent purchases.")]
        CREDIT_CARD,
        [Display(Name = "Google Pay", Description = "Do you have a device with an Android operating system? In this case, use Google Pay without providing payment data. This is simple and convenient, and importantly - your card data is not stored on the device and not transferred during the transaction.")]
        GOOGLE_PAY,
        [Display(Name = "Apple Pay", Description = "If you are a user of an iOS device, Apple Pay is built-in. This does not require downloading any separate application. In addition, payment with Apple Pay is also possible in the Safari browser.")]
        APPLE_PAY,
        [Display(Name = "PayPal", Description = "PayPal is a global payment method that allows you to pay anywhere in the world without revealing your financial data. Simply top up your PayPal or use your credit or debit card. You can also pay by card once without having to login.Simply top up your PayPal or use a credit or debit card. You can also pay by card once without having to log in.")]
        PAYPAL,
        [Display(Name = "Pay on delivery", Description = "If you choose the shipping method, you can choose the payment on receipt. This means that you will pay the goods by cash or payment card to the courier who will deliver the parcel to you or at the point of receipt.")]
        PAY_ON_DELIVERY
    }
}
