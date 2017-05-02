using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Interfaces;
using Newtonsoft.Json;

namespace Kapitalist.Data.Models
{
    [ComplexType]
    public class Project : IComplexType, IProject
    {
        [JsonProperty("id")]
        [StringLength(128)]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        public bool IsEmpty()
        {
            throw new NotImplementedException();
        }
    }
}
