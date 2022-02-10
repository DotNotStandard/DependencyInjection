/*
 * Copyright © 2022 DotNotStandard. All rights reserved.
 * 
 * See the LICENSE file in the root of the repo for licensing details.
 * 
 */
using DotNotStandard.DependencyInjection.AutoDiscovery;
using DotNotStandard.DependencyInjection.AutoDiscovery.Filters;
using DotNotStandard.DependencyInjection.AutoDiscovery.Registrars;
using DotNotStandard.DependencyInjection.AutoDiscovery.ServiceTypeSelectors;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{

	/// <summary>
	/// Builder for type discovery options; this is the class through which we do fluent configuration
	/// </summary>
	public class TypeDiscoveryOptionsBuilder
	{
		private int defaultExclusionsCount = 0;
		private readonly IServiceCollection _services;
		private readonly TypeDiscoveryOptions _options = new TypeDiscoveryOptions();

		#region Constructors

		public TypeDiscoveryOptionsBuilder(IServiceCollection services, Assembly targetAssembly)
		{
			if (services is null) throw new ArgumentNullException(nameof(services));
			if (targetAssembly is null) throw new ArgumentNullException(nameof(targetAssembly));

			_services = services;
			_options.TargetAssembly = targetAssembly;

			// Add default exclusions
			_options.Exclusions.Add(new ExpressionFilter(t => t.IsAbstract));
			_options.Exclusions.Add(new ExpressionFilter(t => t.IsInterface));
			defaultExclusionsCount = _options.Exclusions.Count;
		}

		#endregion

		#region Type Discovery

		/// <summary>
		/// Apply a filter to the types that are included using an expression
		/// </summary>
		/// <param name="inclusion">The expression to use to recognise included types</param>
		/// <returns>The TypeDiscoveryBuilder instance, to support method chaining</returns>
		public TypeDiscoveryOptionsBuilder Where(Func<Type, bool> inclusion)
		{
			if (inclusion is not null)
			{
				_options.Inclusions.Add(new ExpressionFilter(inclusion));
			}

			return this;
		}

		/// <summary>
		/// Apply a filter to the types that are included using an instance of an IFilter
		/// </summary>
		/// <param name="filter">The filter instance to use to recognise included types</param>
		/// <returns>The TypeDiscoveryBuilder instance, to support method chaining</returns>
		public TypeDiscoveryOptionsBuilder Where(IFilter filter)
		{
			if (filter is not null)
			{
				_options.Inclusions.Add(filter);
			}

			return this;
		}

		/// <summary>
		/// Apply a filter to the types that are excluded using an expression
		/// </summary>
		/// <param name="exclusion">The expression to use to recognise excluded types</param>
		/// <returns>The TypeDiscoveryBuilder instance, to support method chaining</returns>
		public TypeDiscoveryOptionsBuilder WhereNot(Func<Type, bool> exclusion)
		{
			if (exclusion is not null)
			{
				_options.Exclusions.Add(new ExpressionFilter(exclusion));
			}

			return this;
		}

		/// <summary>
		/// Apply a filter to the types that are included using an instance of an IFilter
		/// </summary>
		/// <param name="filter">The IFilter instance to use to recognise excluded types</param>
		/// <returns>The TypeDiscoveryBuilder instance, to support method chaining</returns>
		public TypeDiscoveryOptionsBuilder WhereNot(IFilter filter)
		{
			if (filter is not null)
			{
				_options.Exclusions.Add(filter);
			}

			return this;
		}

		/// <summary>
		/// Remove the default exclusions that are applied - IsAbstract and IsInterface
		/// </summary>
		/// <returns>The TypeDiscoveryBuilder instance, to support method chaining</returns>
		public TypeDiscoveryOptionsBuilder ClearDefaultExclusions()
		{
			while (defaultExclusionsCount > 0)
			{
				_options.Exclusions.RemoveAt(0);
				defaultExclusionsCount--;
			}

			return this;
		}

		#endregion

		#region Service Type Identification

		/// <summary>
		/// Specify that types should be registered as their own service types
		/// In other words, we specify this to be able to request types by themselves
		/// </summary>
		/// <remarks>
		/// This is the default behaviour; if nothing else is specified, this is how types 
		/// will be registered. As a result, calling this is optional
		/// </remarks>
		/// <returns>The TypeDiscoveryBuilder instance, to support method chaining</returns>
		public TypeDiscoveryOptionsBuilder AsThemselves()
		{
			_options.ServiceTypeSelector = new SelfServingServiceTypeSelector();

			return this;
		}

		/// <summary>
		/// Specify that types should be registered against interfaces that they implement, by name
		/// In other words, we specify this to be able to request types by the interface they implement
		/// </summary>
		/// <returns>The TypeDiscoveryBuilder instance, to support method chaining</returns>
		public TypeDiscoveryOptionsBuilder AsSimilarlyNamedInterface()
		{
			_options.ServiceTypeSelector = new SimilarlyNamedInterfaceServiceTypeSelector();

			return this;
		}

		/// <summary>
		/// Specify that types should be registered against interfaces that they implement, by name, excluding a prefix
		/// In other words, we specify this to be able to request types by the interface they implement
		/// but where the interface does not include a prefix that the type itself carries (e.g. Fake, Mock)
		/// </summary>
		/// <returns>The TypeDiscoveryBuilder instance, to support method chaining</returns>
		public TypeDiscoveryOptionsBuilder AsSimilarlyNamedInterfaceLessPrefix(string prefix)
		{
			_options.ServiceTypeSelector = new SimilarlyNamedInterfaceLessPrefixServiceTypeSelector(prefix);

			return this;
		}

		/// <summary>
		/// Specify that types should be registered as a specific type that is provided
		/// In other words, we specify this to be able to request types not by themselves, but 
		/// by specifying the type that is provided, which might be an interface or an abstract class
		/// </summary>
		/// <returns>The TypeDiscoveryBuilder instance, to support method chaining</returns>
		public TypeDiscoveryOptionsBuilder AsServiceType<T>() where T: class
		{
			_options.ServiceTypeSelector = new SpecifiedTypeServiceTypeSelector(typeof(T));

			return this;
		}

		/// <summary>
		/// Specify that types should be registered using a custom class to decide how the 
		/// type against which they are registered is selected. This is for advanced scenarios
		/// </summary>
		/// <returns>The TypeDiscoveryBuilder instance, to support method chaining</returns>
		public TypeDiscoveryOptionsBuilder WithCustomServiceTypeSelector(IServiceTypeSelector serviceTypeSelector)
		{
			_options.ServiceTypeSelector = serviceTypeSelector;

			return this;
		}

		#endregion

		#region Lifetime

		/// <summary>
		/// Specify that we want discovered types to be registered with transient lifetime
		/// </summary>
		/// <remarks>
		/// This is the default behaviour; if we do not specify another lifetime then this is 
		/// the lifetime that would be used, so calling this is optional
		/// </remarks>
		/// <returns>The TypeDiscoveryBuilder instance, to support method chaining</returns>
		public TypeDiscoveryOptionsBuilder AsTransient()
		{
			_options.Registrar = new TransientRegistrar();

			return this;
		}

		/// <summary>
		/// Specify that we want discovered types to be registered with scoped lifetime, 
		/// overriding the default lifetime of transient
		/// </summary>
		/// <returns>The TypeDiscoveryBuilder instance, to support method chaining</returns>
		public TypeDiscoveryOptionsBuilder AsScoped()
		{
			_options.Registrar = new ScopedRegistrar();

			return this;
		}

		/// <summary>
		/// Specify that we want discovered types to be registered with singleton lifetime, 
		/// overriding the default lifetime of transient
		/// </summary>
		/// <returns>The TypeDiscoveryBuilder instance, to support method chaining</returns>
		public TypeDiscoveryOptionsBuilder AsSingleton()
		{
			_options.Registrar = new SingletonRegistrar();

			return this;
		}

		#endregion

		#region Exception Behaviour

		/// <summary>
		/// Request that an exception be thrown if a service type is not discovered
		/// </summary>
		/// <remarks>
		/// This is the default behaviour, so calling this is optional.
		/// </remarks>
		/// <returns>The TypeDiscoveryBuilder instance, to support method chaining</returns>
		public TypeDiscoveryOptionsBuilder ThrowingExceptionIfNoServiceTypeIsDiscovered()
		{
			_options.ThrowIfNoServiceTypeIsDiscovered = true;

			return this;
		}

		/// <summary>
		/// Request that any type is ignored for which a service type is not discovered,
		/// avoiding the throwing of an exception that would happen by default
		/// </summary>
		/// <returns>The TypeDiscoveryBuilder instance, to support method chaining</returns>
		public TypeDiscoveryOptionsBuilder IgnoringTypeIfNoServiceTypeIsDiscovered()
		{
			_options.ThrowIfNoServiceTypeIsDiscovered = false;

			return this;
		}

		#endregion

		/// <summary>
		/// The final method of the builder, which triggers discovery and registration
		/// </summary>
		public void Register()
		{
			RegistrationManager registrar = new RegistrationManager();

			registrar.PerformRegistrations(_services, _options);
		}

	}
}