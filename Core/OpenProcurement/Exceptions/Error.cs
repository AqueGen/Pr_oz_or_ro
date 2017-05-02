using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Kapitalist.Core.OpenProcurement.Exceptions
{
	public class Error
	{
		public dynamic Description;
		public string Location;
		public string Name;
    }
}
