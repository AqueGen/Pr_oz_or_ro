using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Drafts.Interfaces;

namespace Kapitalist.Data.Models.Interfaces
{
    public interface IPlan : IDraftPlan, IPublished
    {
        /// <summary>
		/// рядок, генерується автоматично, лише для читання
		/// Ідентифікатор плану, щоб знайти план у “паперовій” документації
		/// </summary>
        string Identifier { get; set; }

        /// <summary>
		/// Власник плану.
		/// Тільки для читання.
		/// </summary>
        string Owner { get; set; }
    }
}
