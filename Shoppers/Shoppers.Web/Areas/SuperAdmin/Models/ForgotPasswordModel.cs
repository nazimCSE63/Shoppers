using System.ComponentModel.DataAnnotations;

namespace Shoppers.Web.Areas.SuperAdmin.Models
{
    public class ForgotPasswordModel
    {

        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
