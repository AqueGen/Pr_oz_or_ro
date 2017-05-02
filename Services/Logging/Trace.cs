using System.Diagnostics;

namespace Kapitalist.Services.Logging
{
    public class Trace : ITrace
    {
        public Trace(string name, SourceLevels defaultLevel = SourceLevels.All)
        {
            Source = new TraceSource(name, defaultLevel);
        }

        public TraceSource Source { get; }
    }
}