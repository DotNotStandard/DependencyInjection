/*
 * Copyright © 2022 DotNotStandard. All rights reserved.
 * 
 * See the LICENSE file in the root of the repo for licensing details.
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DotNotStandard.DependencyInjection.AutoDiscovery.ServiceTypeSelectors
{
	/// <summary>
	/// Service type selector for use when types are registered against an interface
	/// whose name can be calculated from the type name with limited modification
	internal class SimilarlyNamedInterfaceServiceTypeSelector : IServiceTypeSelector
	{
		
		/// <inheritdoc cref="IServiceTypeSelector"/>
		public Type GetServiceType(Type implementingType)
		{
			string desiredInterfaceName;
			Type desiredInterfaceType;
			TypeInfo typeInfo;

			if (implementingType is null) return null;

			// Determine the name of the interface we are expecting to be implemented
			desiredInterfaceName = $"I{implementingType.Name}";

			// Retrieve the interface of this name from those implemented by the type
			// This will return null if the type doesn't implement an interface with an appropriate name
			typeInfo = implementingType.GetTypeInfo();
			desiredInterfaceType = typeInfo.ImplementedInterfaces.FirstOrDefault(
				i => i.Name.Equals(desiredInterfaceName, StringComparison.InvariantCultureIgnoreCase));

			return desiredInterfaceType;
		}
	}
}
