using System.ServiceProcess;

namespace Kapitalist.Services.SyncService
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main()
        {
            ServiceBase[] ServicesToRun = new ServiceBase[]
            {
                new SyncTenders(),
                new SyncPlans()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}