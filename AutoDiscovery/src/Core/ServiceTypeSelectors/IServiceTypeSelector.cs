using System;
using System.Collections.Generic;
using System.Text;

namespace DotNotStandard.DependencyInjection.AutoDiscovery.ServiceTypeSelectors
{
	public interface IServiceTypeSelector
	{

		Type GetServiceType(Type implementingType);

	}
}
