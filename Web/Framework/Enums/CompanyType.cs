using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Kapitalist.Web.Resources;
using Kapitalist.Web.Framework.Helpers.Enums;

namespace Kapitalist.Web.Framework.Enums
{
    [LocalizationEnum(typeof(GlobalRes))]
    public enum CompanyType
    {
        Individual,
        Corporation
    }
}