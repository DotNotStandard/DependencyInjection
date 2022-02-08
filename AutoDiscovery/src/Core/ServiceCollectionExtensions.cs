using DotNotStandard.DependencyInjection.AutoDiscovery;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class ServiceCollectionExtensions
	{

		public static TypeDiscoveryBuilder DiscoverTypes(this IServiceCollection services)
		{
			return new TypeDiscoveryBuilder(services, Assembly.GetExecutingAssembly());
		}

		public static TypeDiscoveryBuilder DiscoverTypesIn(this IServiceCollection services, Assembly assembly)
		{
			return new TypeDiscoveryBuilder(services, assembly);
		}
	}
}
