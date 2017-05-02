using Kapitalist.Data.Store.Models;
using Kapitalist.Data.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapitalist.Services.Prozorro.Mappers
{
    public static class DraftPlanMapper
    {
        public static DraftPlanDTO ToDTO(this DraftPlan source)
        {
            throw new NotImplementedException();
            return source == null ? null
                : new DraftPlanDTO(source)
                {

                };
        }

        public static DraftPlan ToDraftPlan(this DraftPlanDTO source)
        {
            throw new NotImplementedException();
            return source == null ? null
                : new DraftPlan(source)
                {

                };
        }

        public static ItemDTO ToDTO(this DraftPlanItem source)
        {
            throw new NotImplementedException();
            return source == null ? null
                : new ItemDTO(source)
                {

                };
        }

        public static DraftPlanItem ToDraftPlanItem(this ItemDTO source)
        {
            throw new NotImplementedException();
            return source == null ? null
                : new DraftPlanItem(source)
                {

                };
        }
    }
}
