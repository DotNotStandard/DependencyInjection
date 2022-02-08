using System;
using System.Collections.Generic;
using System.Text;

namespace DotNotStandard.DependencyInjection.AutoDiscovery.ServiceTypeSelectors
{
	internal class SelfServingServiceTypeSelector : IServiceTypeSelector
	{
		public Type GetServiceType(Type implementingType)
		{
			return implementingType;
		}
	}
}
