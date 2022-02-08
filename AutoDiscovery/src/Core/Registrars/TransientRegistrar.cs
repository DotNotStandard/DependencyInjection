using Microsoft.Extensions.DependencyInjection;
using System;

namespace DotNotStandard.DependencyInjection.AutoDiscovery.Registrars
{
	internal class TransientRegistrar : IRegistrar
	{
		public void Register(IServiceCollection services, Type serviceType, Type implementingType)
		{
			services.AddTransient(serviceType, implementingType);
		}
	}
}