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
	/// Service type selector for use when types are registered as themselves - i.e. 
	/// where the service type and the implementing type are the same
	/// </summary>
	internal class SelfServingServiceTypeSelector : IServiceTypeSelector
	{
		/// <inheritdoc cref="IServiceTypeSelector"/>
		public Type GetServiceType(Type implementingType)
		{
			return implementingType;
		}
	}
}
