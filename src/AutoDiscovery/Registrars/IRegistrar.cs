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
	/// Contract defining the behaviour of a registrar
	/// </summary>
	internal interface IRegistrar
	{

		/// <summary>
		/// Register a type against the appropriate service type in the DI container
		/// </summary>
		/// <param name="services">The service collection into which to perform registration</param>
		/// <param name="serviceType">The service type against which the implementing type is to be registered</param>
		/// <param name="implementingType">The implementing type to be registered into the DI container</param>
		void Register(IServiceCollection services, Type serviceType, Type implementingType);

	}
}