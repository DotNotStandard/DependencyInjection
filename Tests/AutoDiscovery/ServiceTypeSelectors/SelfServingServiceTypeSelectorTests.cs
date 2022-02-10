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
	public class SelfServingServiceTypeSelectorTests
	{

		#region GetServiceType

		[TestMethod]
		public void GetServiceType_Null_ReturnsNull()
		{
			// Arrange
			IServiceTypeSelector classUnderTest = new SelfServingServiceTypeSelector();
			Type? actual;

			// Act
			actual = classUnderTest.GetServiceType(null);

			// Assert
			Assert.IsNull(actual);

		}

		[TestMethod]
		public void GetServiceType_FakeViewModel_ReturnsFakeViewModel()
		{
			// Arrange
			IServiceTypeSelector classUnderTest = new SelfServingServiceTypeSelector();
			Type expected = typeof(Fakes.ViewModels.FakeViewModel);
			Type actual;

			// Act
			actual = classUnderTest.GetServiceType(typeof(Fakes.ViewModels.FakeViewModel));

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void GetServiceType_String_ReturnsString()
		{
			// Arrange
			IServiceTypeSelector classUnderTest = new SelfServingServiceTypeSelector();
			Type expected = typeof(string);
			Type actual;

			// Act
			actual = classUnderTest.GetServiceType(typeof(string));

			// Assert
			Assert.AreEqual(expected, actual);

		}

		#endregion

		#region Private Helper Methods

		#endregion
	}

}