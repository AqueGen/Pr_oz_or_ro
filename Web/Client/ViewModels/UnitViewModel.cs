using System.ComponentModel.DataAnnotations;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Data.Models;

namespace Kapitalist.Web.Client.ViewModels
{
    public class UnitViewModel
    {
        [Required]
        public string Code { get; set; }

        public string Name { get; set; }

        public UnitViewModel(Unit unit)
        {
            if (unit != null)
            {
                Name = unit.Name;
                Code = unit.Code;
            }
        }

        public UnitViewModel()
        {
        }

        public Unit ToDTO()
        {
            return new Unit()
            {
                Name = Name,
                Code = Code
            };
        }
    }
}