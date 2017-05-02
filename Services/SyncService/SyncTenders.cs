using Kapitalist.Services.Prozorro;
using Kapitalist.Services.Prozorro.Interfaces;
using System;
using System.Diagnostics;
using System.ServiceProcess;

namespace Kapitalist.Services.SyncService
{
    public partial class SyncTenders : ServiceBase, ISyncService
    {
        public SyncTenders()
        {
            InitializeComponent();
            _controller = new SyncController<TendersSyncEngine>(this);
            _controller.Trace = new Logging.Trace(ServiceName);
            _controller.Trace.Source.Listeners.Add(new EventLogTraceListener(ServiceName));
        }

        private readonly ISyncController _controller;

        public int PageSize { get { return 100; } }

        public TimeSpan DefaultTimeout { get { return TimeSpan.FromSeconds(30); } }

        public TimeSpan MaxTimeout { get { return TimeSpan.FromMinutes(30); } }

        public int MaxReties { get { return 5; } }

        protected override void OnStart(string[] args)
        {
            _controller.OnStart(args);
        }

        protected override void OnStop()
        {
            _controller.OnStop();
        }

        protected override void OnPause()
        {
            _controller.OnPause();
            base.OnPause();
        }

        protected override void OnContinue()
        {
            _controller.OnContinue();
            base.OnContinue();
        }
    }
}