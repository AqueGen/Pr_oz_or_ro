using System;
using System.ComponentModel.DataAnnotations;
using Kapitalist.Data.Models;
using Kapitalist.Web.Client.Interfaces;

namespace Kapitalist.Web.Client.ViewModels
{
    public class MinimalStepViewModel : ValueViewModel, IMinimalStepViewModel
    {
        private readonly decimal _lotValue;

        public MinimalStepViewModel()
        {
        }

        public MinimalStepViewModel(decimal lotValue, Value value) : base(value)
        {
            _lotValue = lotValue;
        }

        [Range(0, int.MaxValue)]
        public int Percentage
        {
            get
            {
                if(Amount > 0)
                {
                    if (_lotValue > 0)
                    {
                        return Convert.ToInt32(Amount / (_lotValue / 100));
                    }
                }
                return 0;
            }
        }
    }
}