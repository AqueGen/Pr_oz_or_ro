using System;
using Kapitalist.Data.Store;
using Kapitalist.Web.Security;

namespace Kapitalist.Services.Prozorro.Providers
{
	public class BaseProvider
	{
		protected StoreContext Context { get; }

        protected IAccessManager AccessManager { get; }

        public BaseProvider(StoreContext context, IAccessManager accessManager)
		{
            Context = context;
            AccessManager = accessManager;
		}
	}
}