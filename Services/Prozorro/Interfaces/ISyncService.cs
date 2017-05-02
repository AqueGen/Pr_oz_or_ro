using System;

namespace Kapitalist.Services.Prozorro.Interfaces
{
    public interface ISyncService
    {
        string ServiceName { get; set; }

        int PageSize { get; }

        TimeSpan DefaultTimeout { get; }

        TimeSpan MaxTimeout { get; }

        int MaxReties { get; }

        void Stop();
    }
}