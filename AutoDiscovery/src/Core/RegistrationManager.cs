using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNotStandard.DependencyInjection.AutoDiscovery
{
	internal class RegistrationManager
	{

		internal void PerformRegistrations(IServiceCollection services, TypeDiscoveryOptions discoveryOptions)
		{
			Type serviceType;
			IEnumerable<Type> registerableTypes;
			TypeDiscoverer discoverer = new TypeDiscoverer();

			registerableTypes = discoverer.FindMatchingTypes(discoveryOptions);
			foreach (Type implementingType in registerableTypes)
			{
				serviceType = discoveryOptions.ServiceTypeSelector.GetServiceType(implementingType);
				if (serviceType is null)
				{
					if (discoveryOptions.ThrowIfNoServiceTypeIsDiscovered)
					{
						throw new InvalidOperationException($"No service type could be discovered for {implementingType.FullName}");
					}
					continue;
				}

				// All good; register the type now
				discoveryOptions.Registrar.Register(services, serviceType, implementingType);
			}
		}
	}
}
