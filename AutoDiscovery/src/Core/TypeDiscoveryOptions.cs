/*
 * Copyright © 2022 DotNotStandard. All rights reserved.
 * 
 * See the LICENSE file in the root of the repo for licensing details.
 * 
 */
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

	/// <summary>
	/// DTO for transferring discovery options between components
	/// </summary>
	internal class TypeDiscoveryOptions
	{

		public bool ThrowIfNoServiceTypeIsDiscovered { get; internal set; } = true;

		public Assembly TargetAssembly { get; internal set; }

		public IList<IFilter> Exclusions { get; private set; } = new List<IFilter>();

		public IList<IFilter> Inclusions { get; private set; } = new List<IFilter>();

		public IRegistrar Registrar { get; internal set; } = new Registrars.TransientRegistrar();

		public IServiceTypeSelector ServiceTypeSelector { get; internal set; } = new ServiceTypeSelectors.SelfServingServiceTypeSelector();

	}
}
