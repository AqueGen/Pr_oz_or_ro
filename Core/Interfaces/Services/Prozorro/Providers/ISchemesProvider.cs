using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kapitalist.Services.Prozorro.Providers
{
    public interface ISchemesProvider
    {
        Task<IEnumerable<string>> GetIdentifierSchemes();
    }
}