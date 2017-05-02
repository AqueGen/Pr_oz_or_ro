using Kapitalist.Services.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapitalist.Services.Prozorro.Interfaces
{
    public interface ISyncController
    {
        void OnStart(string[] args);

        void OnStop();

        void OnPause();

        void OnContinue();

        ITrace Trace { get; set; }
    }
}
