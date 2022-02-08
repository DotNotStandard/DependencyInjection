using DotNotStandard.DependencyInjection.AutoDiscovery.Filters;
using DotNotStandard.DependencyInjection.AutoDiscovery.Registrars;
using DotNotStandard.DependencyInjection.AutoDiscovery.ServiceTypeSelectors;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DotNotStandard.DependencyInjection.AutoDiscovery
{
	public class TypeDiscoveryBuilder
	{
		private readonly IServiceCollection _services;
		private readonly Assembly _targetAssembly;
		private readonly IList<IFilter> _exclusions = new List<IFilter>();
		private readonly IList<IFilter> _inclusions = new List<IFilter>();
		private bool _throwIfNoServiceTypeDiscovered = true;
		private IRegistrar _registrar = new TransientRegistrar();
		private IServiceTypeSelector _serviceTypeSelector = new SelfServingServiceTypeSelector();

		public TypeDiscoveryBuilder(IServiceCollection services, Assembly targetAssembly)
		{
			_services = services;
			_targetAssembly = targetAssembly;

			// Add default exclusions
			_exclusions.Add(new FuncFilter(t => t.IsAbstract));
			_exclusions.Add(new FuncFilter(t => t.IsInterface));
		}

		#region Type Discovery

		public TypeDiscoveryBuilder Where(Func<Type, bool> inclusion)
		{
			if (inclusion is not null)
			{
				_inclusions.Add(new FuncFilter(inclusion));
			}

			return this;
		}

		public TypeDiscoveryBuilder Where(IFilter filter)
		{
			if (filter is not null)
			{
				_inclusions.Add(filter);
			}

			return this;
		}

		public TypeDiscoveryBuilder WhereNot(Func<Type, bool> exclusion)
		{
			if (exclusion is not null)
			{
				_exclusions.Add(new FuncFilter(exclusion));
			}

			return this;
		}

		public TypeDiscoveryBuilder WhereNot(IFilter filter)
		{
			if (filter is not null)
			{
				_exclusions.Add(filter);
			}

			return this;
		}

		#endregion

		#region Service Type Identification

		public TypeDiscoveryBuilder AsTheirOwnImplementers()
		{
			_serviceTypeSelector = new SelfServingServiceTypeSelector();

			return this;
		}

		public TypeDiscoveryBuilder AsSimilarlyNamedInterface()
		{
			_serviceTypeSelector = new SimilarlyNamedInterfaceServiceSelector();

			return this;
		}

		public TypeDiscoveryBuilder WithCustomServiceTypeSelector(IServiceTypeSelector serviceTypeSelector)
		{
			_serviceTypeSelector = serviceTypeSelector;

			return this;
		}

		#endregion

		#region Scope

		public TypeDiscoveryBuilder AsTransient()
		{
			_registrar = new TransientRegistrar();

			return this;
		}

		public TypeDiscoveryBuilder AsScoped()
		{
			_registrar = new ScopedRegistrar();

			return this;
		}

		public TypeDiscoveryBuilder AsSingleton()
		{
			_registrar = new SingletonRegistrar();

			return this;
		}

		#endregion

		#region Exception Behaviour

		public TypeDiscoveryBuilder ThrowExceptionIfNoServiceTypeIsDiscovered()
		{
			_throwIfNoServiceTypeDiscovered = true;

			return this;
		}

		public TypeDiscoveryBuilder SuppressExceptionIfNoServiceTypeIsDiscovered()
		{
			_throwIfNoServiceTypeDiscovered = false;

			return this;
		}

		#endregion

		public void Register()
		{
			RegistrationManager registrar = new RegistrationManager();

			registrar.PerformRegistrations(_services, GetOptions());
		}

		private TypeDiscoveryOptions GetOptions()
		{
			return new TypeDiscoveryOptions()
			{
				ThrowIfNoServiceTypeIsDiscovered = _throwIfNoServiceTypeDiscovered,
				TargetAssembly = _targetAssembly,
				Inclusions = _inclusions,
				Exclusions = _exclusions,
				Registrar = _registrar,
				ServiceTypeSelector = _serviceTypeSelector
			};
		}
	}
}