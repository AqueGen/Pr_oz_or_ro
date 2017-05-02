using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Drafts.Interfaces;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Core.OpenProcurement.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Item : BaseItem
    {
        public Item()
        {
        }

        public Item(IDraftItem item)
            : base(item)
        {
        }

        public Item(IItem item)
            : base(item)
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
        /// StringId пов’язаного Lot.
        /// </summary>
        [JsonProperty("relatedLot")]
        public string RelatedLot { get; set; }
    }
}
