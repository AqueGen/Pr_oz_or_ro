using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Data.Models.Enums;
using Kapitalist.Web.Client.Interfaces;
using Kapitalist.Web.Client.ViewModels.Profile;
using Kapitalist.Web.Framework.Attributes;
using Kapitalist.Web.Framework.Enums;

namespace Kapitalist.Web.Client.ViewModels.Account
{
    public class RegisterViewModel : PersonalViewModel
    {
        public RegisterViewModel()
        {
        }

        public RegisterViewModel(OrganizationDTO userOrganizationDTO) : base(userOrganizationDTO)
        {
        }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        //TODO 8 символов, 1 маленькая, 1 большая, 1 символ
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}