using System.ComponentModel.DataAnnotations;

namespace Kapitalist.Web.Client.Models.Identity
{
    public class EditRoleModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Назва (латинецею)")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Назва (кирилицею)")]
        public string NameCyrillic { get; set; }

        [Required]
        [Display(Name = "Опис")]
        public string Description { get; set; }
    }

    public class CreateRoleModel
    {
        [Required]
        [Display(Name = "Назва (латинецею)")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Назва (кирилицею)")]
        public string NameCyrillic { get; set; }

        [Required]
        [Display(Name = "Опис")]
        public string Description { get; set; }
    }  
}