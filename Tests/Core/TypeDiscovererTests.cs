/*
 * Copyright © 2022 DotNotStandard. All rights reserved.
 * 
 * See the LICENSE file in the root of the repo for licensing details.
 * 
 */
using DotNotStandard.DependencyInjection.AutoDiscovery.Filters;
using DotNotStandard.DependencyInjection.AutoDiscovery.ServiceTypeSelectors;
using DotNotStandard.DependencyInjection.AutoDiscovery.UnitTests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNotStandard.DependencyInjection.AutoDiscovery.UnitTests
{

	[TestClass]
	public class TypeDiscovererTests
	{

		#region FindMatchingTypes

		[TestMethod]
		public void FindMatchingTypes_ExclusionsIncludesHardcodedTrue_ReturnsEmptyIEnumerable()
		{
			// Arrange
			TypeDiscoverer classUnderTest = new TypeDiscoverer();
			TypeDiscoveryOptions discoveryOptions = GetInitialisedDiscoveryOptions();
			discoveryOptions.Exclusions.Add(new ExpressionFilter(t => true));
			int expected = 0;
			int actual;
			IEnumerable<Type> results;

			// Act
			results = classUnderTest.FindMatchingTypes(discoveryOptions);
			actual = results.Count();

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void FindMatchingTypes_ExclusionsIncludesHardcodedFalseAndHarcodedTrueInclusionsHasHardcodedTrue_ReturnsEmptyIEnumerable()
		{
			// Arrange
			TypeDiscoverer classUnderTest = new TypeDiscoverer();
			TypeDiscoveryOptions discoveryOptions = GetInitialisedDiscoveryOptions();
			discoveryOptions.Exclusions.Add(new ExpressionFilter(t => false));
			discoveryOptions.Exclusions.Add(new ExpressionFilter(t => true));
			discoveryOptions.Inclusions.Add(new ExpressionFilter(t => true));
			int expected = 0;
			int actual;
			IEnumerable<Type> results;

			// Act
			results = classUnderTest.FindMatchingTypes(discoveryOptions);
			actual = results.Count();

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void FindMatchingTypes_ExclusionsIncludesHardcodedFalseInclusionsHasHardcodedTrue_ReturnsNonEmptyIEnumerable()
		{
			// Arrange
			TypeDiscoverer classUnderTest = new TypeDiscoverer();
			TypeDiscoveryOptions discoveryOptions = GetInitialisedDiscoveryOptions();
			discoveryOptions.Exclusions.Add(new ExpressionFilter(t => false));
			discoveryOptions.Inclusions.Add(new ExpressionFilter(t => true));
			int notExpected = 0;
			int actual;
			IEnumerable<Type> results;

			// Act
			results = classUnderTest.FindMatchingTypes(discoveryOptions);
			actual = results.Count();

			// Assert
			Assert.AreNotEqual(notExpected, actual);

		}

		[TestMethod]
		public void FindMatchingTypes_InclusionsHasEndsWithRenegitory_ReturnsEmptyIEnumerable()
		{
			// Arrange
			TypeDiscoverer classUnderTest = new TypeDiscoverer();
			TypeDiscoveryOptions discoveryOptions = GetInitialisedDiscoveryOptions();
			discoveryOptions.Inclusions.Add(new ExpressionFilter(t => t.Name.EndsWith("Renegitory")));
			int expected = 0;
			int actual;
			IEnumerable<Type> results;

			// Act
			results = classUnderTest.FindMatchingTypes(discoveryOptions);
			actual = results.Count();

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void FindMatchingTypes_InclusionsHasEndsWithRepository_ReturnsIEnumerableWithFourItems()
		{
			// Arrange
			TypeDiscoverer classUnderTest = new TypeDiscoverer();
			TypeDiscoveryOptions discoveryOptions = GetInitialisedDiscoveryOptions();
			discoveryOptions.Inclusions.Add(new ExpressionFilter(t => t.Name.EndsWith("Repository")));
			int expected = 4;
			int actual;
			IEnumerable<Type> results;

			// Act
			results = classUnderTest.FindMatchingTypes(discoveryOptions);
			actual = results.Count();

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void FindMatchingTypes_ExcludeInterfacesInclusionsHasEndsWithRepository_ReturnsIEnumerableWithTwoItems()
		{
			// Arrange
			TypeDiscoverer classUnderTest = new TypeDiscoverer();
			TypeDiscoveryOptions discoveryOptions = GetInitialisedDiscoveryOptions();
			discoveryOptions.Exclusions.Add(new ExpressionFilter(t => t.IsInterface));
			discoveryOptions.Inclusions.Add(new ExpressionFilter(t => t.Name.EndsWith("Repository")));
			int expected = 2;
			int actual;
			IEnumerable<Type> results;

			// Act
			results = classUnderTest.FindMatchingTypes(discoveryOptions);
			actual = results.Count();

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void FindMatchingTypes_ExcludeInterfacesInclusionsHasEndsWithRepository_IncludesFakeReadOnlyListRepository()
		{
			// Arrange
			TypeDiscoverer classUnderTest = new TypeDiscoverer();
			TypeDiscoveryOptions discoveryOptions = GetInitialisedDiscoveryOptions();
			discoveryOptions.Exclusions.Add(new ExpressionFilter(t => t.IsInterface));
			discoveryOptions.Inclusions.Add(new ExpressionFilter(t => t.Name.EndsWith("Repository")));
			Type expected = typeof(Fakes.Repositories.FakeReadOnlyListIdAndNameRepository);
			IEnumerable<Type> results;

			// Act
			results = classUnderTest.FindMatchingTypes(discoveryOptions);

			// Assert
			Assert.IsTrue(results.Contains(expected));

		}

		[TestMethod]
		public void FindMatchingTypes_ExcludeInterfacesInclusionsHasEndsWithRepository_IncludesFakeIdAndNameRepository()
		{
			// Arrange
			TypeDiscoverer classUnderTest = new TypeDiscoverer();
			TypeDiscoveryOptions discoveryOptions = GetInitialisedDiscoveryOptions();
			discoveryOptions.Exclusions.Add(new ExpressionFilter(t => t.IsInterface));
			discoveryOptions.Inclusions.Add(new ExpressionFilter(t => t.Name.EndsWith("Repository")));
			Type expected = typeof(Fakes.Repositories.FakeIdAndNameRepository);
			IEnumerable<Type> results;

			// Act
			results = classUnderTest.FindMatchingTypes(discoveryOptions);

			// Assert
			Assert.IsTrue(results.Contains(expected));

		}

		[TestMethod]
		public void FindMatchingTypes_ExcludeAbstractsInclusionsHasEndsWithViewModel_IncludesFakeViewModel()
		{
			// Arrange
			TypeDiscoverer classUnderTest = new TypeDiscoverer();
			TypeDiscoveryOptions discoveryOptions = GetInitialisedDiscoveryOptions();
			discoveryOptions.Exclusions.Add(new ExpressionFilter(t => t.IsAbstract));
			discoveryOptions.Inclusions.Add(new ExpressionFilter(t => t.Name.EndsWith("ViewModel")));
			Type expected = typeof(Fakes.ViewModels.FakeViewModel);
			IEnumerable<Type> results;

			// Act
			results = classUnderTest.FindMatchingTypes(discoveryOptions);

			// Assert
			Assert.IsTrue(results.Contains(expected));

		}

		#endregion

		#region Private Helper Methods

		/// <summary>
		/// Get a pre-initialised set of discovery options for test usage
		/// </summary>
		/// <returns>A default, pre-initialised object with no null values</returns>
		private TypeDiscoveryOptions GetInitialisedDiscoveryOptions()
		{
			TypeDiscoveryOptions discoveryOptions = new TypeDiscoveryOptions();

			discoveryOptions.ThrowIfNoServiceTypeIsDiscovered = true;
			discoveryOptions.TargetAssembly = typeof(TypeDiscovererTests).Assembly;
			discoveryOptions.Registrar = new FakeRegistrar();
			discoveryOptions.ServiceTypeSelector = new SelfServingServiceTypeSelector();

			return discoveryOptions;
		}

		#endregion
	}

}