using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Kapitalist.Data.Models;
using Kapitalist.Web.Client.Interfaces;

namespace Kapitalist.Web.Client.ViewModels
{
    public class ValueViewModel : IValueViewModel
    {
        public ValueViewModel()
        {
        }

        public ValueViewModel(Value value) : this()
        {
            if (value != null)
            {
                Amount = value.Amount;
                Currency = value.Currency;
                VATIncluded = value.VATIncluded;
            }
        }

        [Range(0, int.MaxValue)]
        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public bool VATIncluded { get; set; }

        public virtual Value ToDTO()
        {
            return new Value
            {
                Currency = Currency,
                VATIncluded = VATIncluded,
                Amount = Amount
            };
        }
    }
}