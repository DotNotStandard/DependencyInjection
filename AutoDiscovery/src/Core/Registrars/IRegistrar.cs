using Microsoft.Extensions.DependencyInjection;
using System;

namespace DotNotStandard.DependencyInjection.AutoDiscovery.Registrars
{
	internal interface IRegistrar
	{

		void Register(IServiceCollection services, Type serviceType, Type implementingType);

	}
}