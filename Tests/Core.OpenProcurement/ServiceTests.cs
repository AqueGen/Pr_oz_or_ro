using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kapitalist.Core.OpenProcurement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Core.OpenProcurement.Models;
using Kapitalist.Core.OpenProcurement.Exceptions;

namespace Tests.Kapitalist.Core.OpenProcurement
{
	public abstract class ServiceTests<T> : IDisposable
		where T : IDisposable
	{
		protected readonly T Service;

		public ServiceTests(T service)
		{
			Service = service;
		}

		public void Dispose()
		{
			Service?.Dispose();
		}
	}
}