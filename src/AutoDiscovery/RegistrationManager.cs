/*
 * Copyright © 2022 DotNotStandard. All rights reserved.
 * 
 * See the LICENSE file in the root of the repo for licensing details.
 * 
 */
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNotStandard.DependencyInjection.AutoDiscovery
{

	/// <summary>
	/// Registration management class, bringing together discovery and registration
	/// </summary>
	internal class RegistrationManager
	{

		/// <summary>
		/// Perform type discovery and registration based upon the pre-defined options
		/// </summary>
		/// <param name="services">The service collection into which to register types</param>
		/// <param name="discoveryOptions">The configuration with which to carry out our work</param>
		/// <exception cref="InvalidOperationException">Raised when no service type is if throwing of exceptions is enabled</exception>
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
