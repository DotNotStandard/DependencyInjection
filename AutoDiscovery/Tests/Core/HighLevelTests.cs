/*
 * Copyright © 2022 DotNotStandard. All rights reserved.
 * 
 * See the LICENSE file in the root of the repo for licensing details.
 * 
 */
using DotNotStandard.DependencyInjection.AutoDiscovery.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNotStandard.DependencyInjection.AutoDiscovery.UnitTests
{

	[TestClass]
	public class HighLevelTests
	{

		#region DiscoverTypes

		[TestMethod]
		public void DiscoverTypes_WhereEndsWithViewModel_RegistersOneType()
		{
			// Arrange
			IServiceCollection services = new ServiceCollection();
			int expected = 1;
			int actual;

			// Act
			services.DiscoverTypes()
				.Where(new NameEndsWithFilter("ViewModel"))
				.AsTheirOwnImplementers()
				.Register();
			actual = services.Count();

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void DiscoverTypes_WhereEndsWithViewModel_RegistersFakeViewModel()
		{
			// Arrange
			IServiceCollection services = new ServiceCollection();
			Type expected = typeof(Fakes.ViewModels.FakeViewModel);
			Type? actual;

			// Act
			services.DiscoverTypes()
				.Where(new NameEndsWithFilter("ViewModel"))
				.AsTheirOwnImplementers()
				.Register();
			actual = services.First()?.ServiceType;

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void DiscoverTypes_WhereEndsWithRepositoryIgnoringTypesWithoutServiceTypeDiscovered_RegistersOneType()
		{
			// Arrange
			IServiceCollection services = new ServiceCollection();
			int expected = 1;
			int actual;

			// Act
			services.DiscoverTypes()
				.Where(new NameEndsWithFilter("Repository"))
				.AsSimilarlyNamedInterface()
				.IgnoringIfNoServiceTypeIsDiscovered()
				.Register();
			actual = services.Count();

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void DiscoverTypes_WhereEndsWithRepositoryIgnoringTypesWithoutServiceTypeDiscovered_RegistersIFakeIdAndNameRepository()
		{
			// Arrange
			IServiceCollection services = new ServiceCollection();
			Type expected = typeof(Fakes.Repositories.IFakeIdAndNameRepository);
			Type? actual;

			// Act
			services.DiscoverTypes()
				.Where(new NameEndsWithFilter("Repository"))
				.AsSimilarlyNamedInterface()
				.IgnoringIfNoServiceTypeIsDiscovered()
				.Register();

			actual = services.First()?.ServiceType;

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void DiscoverTypes_WhereEndsWithRepositoryNoExceptionBehaviourSpecified_ThrowsException()
		{
			// Arrange
			IServiceCollection services = new ServiceCollection();

			// Act
			services.DiscoverTypes()
				.Where(new NameEndsWithFilter("Repository"))
				.AsSimilarlyNamedInterface()
				.Register();
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void DiscoverTypes_WhereEndsWithRepositoryWithExceptionsEnabled_ThrowsException()
		{
			// Arrange
			IServiceCollection services = new ServiceCollection();

			// Act
			services.DiscoverTypes()
				.Where(new NameEndsWithFilter("Repository"))
				.AsSimilarlyNamedInterface()
				.ThrowingExceptionIfNoServiceTypeIsDiscovered()
				.Register();
		}

		[TestMethod]
		public void DiscoverTypes_WhereEndsWithRepositoryAsSimilarlyNamedInterfaceLessPrefixIgnoringExceptions_RegistersOneType()
		{
			// Arrange
			IServiceCollection services = new ServiceCollection();
			int expected = 1;
			int actual;

			// Act
			services.DiscoverTypes()
				.Where(new NameEndsWithFilter("Repository"))
				.AsSimilarlyNamedInterfaceLessPrefix("Fake")
				.IgnoringIfNoServiceTypeIsDiscovered()
				.Register();
			actual = services.Count();

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void DiscoverTypes_WhereEndsWithRepositoryAsSimilarlyNamedInterfaceLessPrefixIgnoringExceptions_RegistersIReadOnlyListIdAndNameRepository()
		{
			// Arrange
			IServiceCollection services = new ServiceCollection();
			Type expected = typeof(Fakes.Repositories.IReadOnlyListIdAndNameRepository);
			Type? actual;

			// Act
			services.DiscoverTypes()
				.Where(new NameEndsWithFilter("Repository"))
				.AsSimilarlyNamedInterfaceLessPrefix("Fake")
				.IgnoringIfNoServiceTypeIsDiscovered()
				.Register();
			actual = services.First()?.ServiceType;

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void DiscoverTypes_WhereStartsWithFakeReadOnlyAndEndsWithRepositoryAsSpecifiedType_RegistersOneType()
		{
			// Arrange
			IServiceCollection services = new ServiceCollection();
			int expected = 1;
			int actual;

			// Act
			services.DiscoverTypes()
				.Where(new NameStartsWithAndEndsWithFilter("FakeReadOnly", "Repository"))
				.AsServiceType<Fakes.Repositories.IReadOnlyListIdAndNameRepository>()
				.Register();
			actual = services.Count();

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void DiscoverTypes_WhereStartsWithFakeReadOnlyAndEndsWithRepositoryAsSpecifiedType_RegistersIReadOnlyListIdAndNameRepository()
		{
			// Arrange
			IServiceCollection services = new ServiceCollection();
			Type expected = typeof(Fakes.Repositories.IReadOnlyListIdAndNameRepository);
			Type? actual;

			// Act
			services.DiscoverTypes()
				.Where(new NameStartsWithAndEndsWithFilter("FakeReadOnly", "Repository"))
				.AsServiceType<Fakes.Repositories.IReadOnlyListIdAndNameRepository>()
				.Register();
			actual = services.First()?.ServiceType;

			// Assert
			Assert.AreEqual(expected, actual);

		}

		#endregion

	}
}
