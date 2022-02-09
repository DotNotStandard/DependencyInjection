/*
 * Copyright © 2022 DotNotStandard. All rights reserved.
 * 
 * See the LICENSE file in the root of the repo for licensing details.
 * 
 */
using DotNotStandard.DependencyInjection.AutoDiscovery.Registrars;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DotNotStandard.DependencyInjection.AutoDiscovery.UnitTests.Fakes
{
	internal class FakeRegistrar : IRegistrar
	{
		public void Register(IServiceCollection services, Type serviceType, Type implementingType)
		{
			// No behaviour required
		}
	}
}