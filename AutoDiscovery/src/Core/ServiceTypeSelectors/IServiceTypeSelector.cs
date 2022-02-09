/*
 * Copyright © 2022 DotNotStandard. All rights reserved.
 * 
 * See the LICENSE file in the root of the repo for licensing details.
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNotStandard.DependencyInjection.AutoDiscovery.ServiceTypeSelectors
{

	/// <summary>
	/// Contract defining the behaviour of a service type selector
	/// </summary>
	public interface IServiceTypeSelector
	{

		/// <summary>
		/// Get the service type for a specific implementation type
		/// </summary>
		/// <param name="implementingType">The type for which a service type is being sought</param>
		/// <returns>The service type for the type requested, or null if no appropriate type could be found</returns>
		Type GetServiceType(Type implementingType);

	}
}
