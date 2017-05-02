using System;

namespace Web.Client.Models.Identity
{
	public class Provider : IDisposable
	{
		private readonly IdentityContext _context;

		protected IdentityContext Context
		{
			get { return _context; }
		}

		public Provider()
		{
			_context = new IdentityContext();
		}

		public void Dispose()
		{
			_context?.Dispose();
		}
	}
}