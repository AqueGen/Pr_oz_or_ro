using Kapitalist.Data.Store;
using Kapitalist.Web.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Kapitalist.Services.Prozorro.Providers
{
    public class SchemesProvider : BaseProvider, ISchemesProvider
    {
        public SchemesProvider(StoreContext context, IAccessManager accessManager)
            : base(context, accessManager)
        {
        }

        public async Task<IEnumerable<string>> GetIdentifierSchemes()
        {
            return await Context.ClassificationSchemes.Select(m => m.Scheme).ToArrayAsync();
        }
    }
}