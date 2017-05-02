using Kapitalist.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapitalist.Data.Models.DTO
{
    public class ContactPointDTO : ContactPointOptional
    {
        public ContactPointDTO()
        {
        }

        public ContactPointDTO(IContactPoint contactPoint)
            : base (contactPoint)
        {
        }

        public int Id { get; set; }
    }
}
