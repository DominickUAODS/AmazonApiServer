using System.ComponentModel.DataAnnotations;

namespace AmazonApiServer.Enums
{
    public enum ProductReviewTag
    {
        [Display(Name = "Actual price")]
        ACTUAL_PRICE,
        [Display(Name = "Fits the sescription")]
        FITS_THE_DESCRIPTION,
        [Display(Name = "High quality")]
        HIGH_QUALITY,
        [Display(Name = "Worth the price")]
        WORTH_THE_PRICE,
        [Display(Name = "Exceeds expectations")]
        EXCEEDS_EXPECTATIONS,
        [Display(Name = "Matches the photos")]
        MATCHES_THE_PHOTOS
        // todo add neutral/negative tags (maybe)
    }
}
