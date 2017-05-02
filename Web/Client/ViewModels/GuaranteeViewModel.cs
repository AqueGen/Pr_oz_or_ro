using System.ComponentModel.DataAnnotations;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Data.Models;

namespace Kapitalist.Web.Client.ViewModels
{
    public class GuaranteeViewModel
    {
        [Range(0, int.MaxValue)]
        public decimal Amount { get; set; }

        [MinLength(3)]
        [StringLength(3)]
        public string Currency { get; set; } = "UAH";

        public GuaranteeViewModel()
        {
        }

        public GuaranteeViewModel(Guarantee guarantee)
        {
            if (guarantee != null)
            {
                Amount = guarantee.Amount;
                Currency = guarantee.Currency;
            }
        }

        public Guarantee ToDTO()
        {
            return new Guarantee
            {
                Amount = Amount,
                Currency = Currency
            };
        }
    }
}