using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kapitalist.Services.SyncService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;
using System.Reflection;
using System.Threading;
using System.Diagnostics;
using Kapitalist.Services.Prozorro.Interfaces;

namespace Tests.Kapitalist.Services.SyncService
{
	[TestClass()]
	public class SyncTendersTests : IDisposable
	{
		protected readonly SyncTenders _service;
		MethodInfo _onStart;
		MethodInfo _onPause;
		MethodInfo _onContinue;

		public SyncTendersTests()
		{
            _service = new SyncTenders();
            _onStart = typeof(SyncTenders).GetMethod("OnStart", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
            _onPause = typeof(SyncTenders).GetMethod("OnPause", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
            _onContinue = typeof(SyncTenders).GetMethod("OnContinue", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
        }

		void OnStart(string args)
		{
			_onStart.Invoke(_service, new[] { args.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries) });
		}

		void OnPause()
		{
			_onPause.Invoke(_service, null);
		}

		void OnContinue()
		{
			_onContinue.Invoke(_service, null);
		}

		[TestMethod()]
		public void SyncTendersTest()
		{
			OnStart("/full /pagesize 5 /timeout  0.2 /maxtimeout 10,0 /maxretries 3");
			Thread.Sleep(2000);
			OnPause();
			OnContinue();
			Thread.Sleep(2000);
			_service.Stop();
			OnStart("");
			_service.Stop();
		}

        public void Dispose()
		{
			_service?.Dispose();
		}
	}
}