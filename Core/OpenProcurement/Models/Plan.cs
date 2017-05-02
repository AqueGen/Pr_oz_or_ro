using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Drafts.Interfaces;
using Kapitalist.Data.Models.Interfaces;
using Newtonsoft.Json;

namespace Kapitalist.Core.OpenProcurement.Models
{
    public class Plan : BasePlan
    {
        public Plan()
        {
        }

        public Plan(IDraftPlan plan)
            : base(plan)
        {
        }

        public Plan(IPlan plan)
            : base(plan)
        {
        }

        /// <summary>
        /// OpenContracting Description: Початкова класифікація елемента. 
        /// Дивіться у itemClassificationScheme, щоб визначити бажані списки класифікації, включно з CPV та GSIN.
        /// Класифікація classification.scheme обов’язково повинна бути CPV. classification.id повинно бути дійсним CPV кодом.
        /// </summary>
        [JsonProperty("classification")]
        public Classification Classification { get; set; }

        /// <summary>
        /// OpenContracting Description: Масив додаткових класифікацій для елемента. 
        /// Дивіться у список кодів itemClassificationScheme, щоб використати поширені варіанти в OCDS. 
        /// Також можна використовувати для представлення кодів з внутрішньої схеми класифікації.
        /// Обов’язково мати хоча б один елемент з ДКПП у стрічці scheme.
        /// </summary>
        [JsonProperty("additionalClassifications")]
        public Classification[] AdditionalClassifications { get; set; }

        /// <summary>
        /// Список, який містить елемент, що закуповується.
        /// OpenContracting Description: Товари та послуги, що будуть закуплені, поділені на спискові елементи, де це можливо.
        /// Елементи не повинні дублюватись, замість цього вкажіть кількість 2.
        /// </summary>
        [JsonProperty("items")]
        public Item[] Items { get; set; }

        /// <summary>
        /// Обов'язково!
        /// Замовник (організація, що проводить закупівлю).
        /// OpenContracting Description: Об’єкт, що управляє закупівлею. Він не обов’язково є покупцем, який платить / використовує закуплені елементи.
        /// </summary>
        [JsonRequired]
        [JsonProperty("procuringEntity")]
        public PlanProcuringEntity ProcuringEntity { get; set; }
    }
}