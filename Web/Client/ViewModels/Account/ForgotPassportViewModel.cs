using System.ComponentModel.DataAnnotations;

namespace Kapitalist.Web.Client.ViewModels.Account
{
    public class ForgotPassportViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}