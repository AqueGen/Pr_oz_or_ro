using Kapitalist.Data.Store.Models;
using Kapitalist.Data.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapitalist.Services.Prozorro.Mappers
{
    public static class PlanMapper
    {
        public static PlanDTO ToDTO(this Plan source)
        {
            throw new NotImplementedException();
            return source == null ? null
                : new PlanDTO(source)
                {

                };
        }

        public static ItemDTO ToDTO(this PlanItem source)
        {
            throw new NotImplementedException();
            return source == null ? null
                : new ItemDTO(source)
                {

                };
        }
    }
}
