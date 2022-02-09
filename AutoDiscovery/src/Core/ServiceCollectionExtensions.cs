/*
 * Copyright © 2022 DotNotStandard. All rights reserved.
 * 
 * See the LICENSE file in the root of the repo for licensing details.
 * 
 */
using DotNotStandard.DependencyInjection.AutoDiscovery;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{

	/// <summary>
	/// Extension methods for the IServiceCollection type, representing the entry point for discovery
	/// </summary>
	public static class ServiceCollectionExtensions
	{

		/// <summary>
		/// Default entry point for type discovery, initiating type discovery on the calling assembly
		/// </summary>
		/// <param name="services">The IServiceCollection instance that we extend</param>
		/// <returns>An instance of TypeDiscoveryOptionsBuilder for configuring and triggering discovery</returns>
		public static TypeDiscoveryOptionsBuilder DiscoverTypes(this IServiceCollection services)
		{
			return new TypeDiscoveryOptionsBuilder(services, Assembly.GetCallingAssembly());
		}

		/// <summary>
		/// Additional entry point for type discovery, initiating type discovery on the specified assembly
		/// </summary>
		/// <param name="services">The IServiceCollection instance that we extend</param>
		/// <returns>An instance of TypeDiscoveryOptionsBuilder for configuring and triggering discovery</returns>
		public static TypeDiscoveryOptionsBuilder DiscoverTypesIn(this IServiceCollection services, Assembly assembly)
		{
			return new TypeDiscoveryOptionsBuilder(services, assembly);
		}
	}
}
