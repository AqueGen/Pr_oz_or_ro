using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Attributes;
using Kapitalist.Data.Models.Drafts;
using Kapitalist.Data.Models.Drafts.Interfaces;
using Kapitalist.Data.Models.Interfaces;
using Newtonsoft.Json;

namespace Kapitalist.Data.Models
{
    public class BasePlan : BaseDraftPlan, IPlan
    {
        public BasePlan()
        {
        }

        public BasePlan(IDraftPlan plan)
            : base(plan)
        {
        }

        public BasePlan(IPlan plan)
            : base(plan)
        {
        }

        /// <summary>
		/// рядок, генерується автоматично, лише для читання
		/// Ідентифікатор плану, щоб знайти план у “паперовій” документації
		/// </summary>
        [JsonProperty("planID")]
		public string Identifier { get; set; }

        public DateTime DatePublished { get; set; }

        /// <summary>
		/// Власник плану.
		/// Тільки для читання.
		/// </summary>
		[JsonProperty("owner")]
        [StringLength(256), Truncate]
        public string Owner { get; set; }
    }
}
