using System.Diagnostics;

namespace Kapitalist.Services.Logging
{
    public interface ITrace
    {
        TraceSource Source { get; }
    }
}