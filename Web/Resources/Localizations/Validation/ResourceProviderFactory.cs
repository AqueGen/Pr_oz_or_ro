using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Web.Compilation;

namespace Kapitalist.Web.Resources.Localizations.Validation
{
	public sealed class ResourceProviderFactory : System.Web.Compilation.ResourceProviderFactory
	{
		public ResourceProviderFactory()
		{
		}

		public override IResourceProvider CreateGlobalResourceProvider(string classKey)
		{
			return new GlobalResourceProvider(classKey);
		}

		public override IResourceProvider CreateLocalResourceProvider(string virtualPath)
		{
			throw new NotImplementedException("Local resources are not supported yet");
		}

		class GlobalResourceProvider : IResourceProvider
		{
			public GlobalResourceProvider(string classKey)
			{
				var type = Type.GetType(classKey, false);
				if (type == null)
				{
					var asmName = classKey;
					var className = classKey;
					while (asmName.IndexOf(".") > -1 && type == null)
					{
						asmName = asmName.Substring(0, asmName.LastIndexOf("."));
						className = classKey.Substring(asmName.Length + 1);
						type = Type.GetType(classKey + "," + asmName, false);
					}
				}
				Manager = CreateResourceManager(classKey, type.Assembly);
			}

			public ResourceManager Manager { get; set; }

			public IResourceReader ResourceReader { get; set; }

			public object GetObject(string resourceKey, CultureInfo culture)
			{
				return Manager.GetObject(resourceKey, culture);
			}

			private ResourceManager CreateResourceManager(string classKey, Assembly assembly)
			{
				return new ResourceManager(classKey, assembly);
			}
		}
	}
}
