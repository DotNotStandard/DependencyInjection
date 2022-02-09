/*
 * Copyright © 2022 DotNotStandard. All rights reserved.
 * 
 * See the LICENSE file in the root of the repo for licensing details.
 * 
 */
using DotNotStandard.DependencyInjection.AutoDiscovery.ServiceTypeSelectors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNotStandard.DependencyInjection.AutoDiscovery.UnitTests.ServiceTypeSelectors
{

	[TestClass]
	public class SimilarlyNamedInterfaceLessPrefixServiceTypeSelectorTests
	{

		#region GetServiceType

		[TestMethod]
		public void GetServiceType_Null_ReturnsNull()
		{
			// Arrange
			IServiceTypeSelector classUnderTest = new SimilarlyNamedInterfaceLessPrefixServiceTypeSelector("Fake");
			Type? actual;

			// Act
			actual = classUnderTest.GetServiceType(null);

			// Assert
			Assert.IsNull(actual);

		}

		[TestMethod]
		public void GetServiceType_FakeIdAndNameRepository_ReturnsNull()
		{
			// Arrange
			IServiceTypeSelector classUnderTest = new SimilarlyNamedInterfaceLessPrefixServiceTypeSelector("Fake");
			Type actual;

			// Act
			actual = classUnderTest.GetServiceType(typeof(Fakes.Repositories.FakeIdAndNameRepository));

			// Assert
			Assert.IsNull(actual);

		}

		[TestMethod]
		public void GetServiceType_FakeReadOnlyListIdAndNameRepository_ReturnsIReadOnlyListIdAndNameRepository()
		{
			// Arrange
			IServiceTypeSelector classUnderTest = new SimilarlyNamedInterfaceLessPrefixServiceTypeSelector("Fake");
			Type expected = typeof(Fakes.Repositories.IReadOnlyListIdAndNameRepository);
			Type actual;

			// Act
			actual = classUnderTest.GetServiceType(typeof(Fakes.Repositories.FakeReadOnlyListIdAndNameRepository));

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void GetServiceType_String_ReturnsNull()
		{
			// Arrange
			IServiceTypeSelector classUnderTest = new SimilarlyNamedInterfaceLessPrefixServiceTypeSelector("Fake");
			Type actual;

			// Act
			actual = classUnderTest.GetServiceType(typeof(string));

			// Assert
			Assert.IsNull(actual);

		}

		#endregion

		#region Private Helper Methods

		#endregion
	}

}