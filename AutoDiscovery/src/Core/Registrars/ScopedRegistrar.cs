using Microsoft.Extensions.DependencyInjection;
using System;

namespace DotNotStandard.DependencyInjection.AutoDiscovery.Registrars
{
	internal class ScopedRegistrar : IRegistrar
	{
		public void Register(IServiceCollection services, Type serviceType, Type implementingType)
		{
			services.AddScoped(serviceType, implementingType);
		}
	}
}