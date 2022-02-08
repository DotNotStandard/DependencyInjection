using DotNotStandard.DependencyInjection.AutoDiscovery.Filters;
using DotNotStandard.DependencyInjection.AutoDiscovery.Registrars;
using DotNotStandard.DependencyInjection.AutoDiscovery.ServiceTypeSelectors;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DotNotStandard.DependencyInjection.AutoDiscovery
{
	internal class TypeDiscoveryOptions
	{

		public bool ThrowIfNoServiceTypeIsDiscovered { get; init; }

		public Assembly TargetAssembly { get; init; }

		public IList<IFilter> Exclusions { get; init; }

		public IList<IFilter> Inclusions { get; init; }

		public IRegistrar Registrar { get; init; }

		public IServiceTypeSelector ServiceTypeSelector { get; init; }

	}
}
