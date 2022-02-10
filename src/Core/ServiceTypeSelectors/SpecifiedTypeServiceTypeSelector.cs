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
	/// Service type selector for use when types are registered against a specific
	/// type that was requested by the calling developer
	internal class SpecifiedTypeServiceTypeSelector : IServiceTypeSelector
	{
		private readonly Type _serviceType;

		#region Constructors

		public SpecifiedTypeServiceTypeSelector(Type serviceType)
		{
			_serviceType = serviceType;
		}

		#endregion

		/// <inheritdoc cref="IServiceTypeSelector"/>
		public Type GetServiceType(Type implementingType)
		{
			return _serviceType;
		}
	}
}
