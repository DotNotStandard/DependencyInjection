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
	public class SimilarlyNamedInterfaceServiceTypeSelectorTests
	{

		#region GetServiceType

		[TestMethod]
		public void GetServiceType_Null_ReturnsNull()
		{
			// Arrange
			IServiceTypeSelector classUnderTest = new SimilarlyNamedInterfaceServiceTypeSelector();
			Type? actual;

			// Act
			actual = classUnderTest.GetServiceType(null);

			// Assert
			Assert.IsNull(actual);

		}

		[TestMethod]
		public void GetServiceType_FakeIdAndNameRepository_ReturnsIFakeIdAndNameRepository()
		{
			// Arrange
			IServiceTypeSelector classUnderTest = new SimilarlyNamedInterfaceServiceTypeSelector();
			Type expected = typeof(Fakes.Repositories.IFakeIdAndNameRepository);
			Type actual;

			// Act
			actual = classUnderTest.GetServiceType(typeof(Fakes.Repositories.FakeIdAndNameRepository));

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void GetServiceType_FakeReadOnlyListIdAndNameRepository_ReturnsNull()
		{
			// Arrange
			IServiceTypeSelector classUnderTest = new SimilarlyNamedInterfaceServiceTypeSelector();
			Type actual;

			// Act
			actual = classUnderTest.GetServiceType(typeof(Fakes.Repositories.FakeReadOnlyListIdAndNameRepository));

			// Assert
			// The name does not match here; implementation is prefixed Fake, the interface not
			Assert.IsNull(actual);

		}

		[TestMethod]
		public void GetServiceType_String_ReturnsNull()
		{
			// Arrange
			IServiceTypeSelector classUnderTest = new SimilarlyNamedInterfaceServiceTypeSelector();
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