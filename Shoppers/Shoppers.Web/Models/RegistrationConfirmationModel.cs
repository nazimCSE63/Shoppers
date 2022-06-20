namespace Shoppers.Web.Models
{
    public class RegistrationConfirmationModel
    {
        public string? Email { get; set; }

        public bool DisplayConfirmAccountLink { get; set; }

        public string? EmailConfirmationUrl { get; set; }
    }
}
