/*
 * Copyright © 2022 DotNotStandard. All rights reserved.
 * 
 * See the LICENSE file in the root of the repo for licensing details.
 * 
 */
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DotNotStandard.DependencyInjection.AutoDiscovery.Registrars
{

	/// <summary>
	/// Registrar that registers types with a singleton lifetime
	/// </summary>
	internal class SingletonRegistrar : IRegistrar
	{
		/// <inheritdoc cref="IRegistrar"/>
		public void Register(IServiceCollection services, Type serviceType, Type implementingType)
		{
			services.AddSingleton(serviceType, implementingType);
		}
	}
}