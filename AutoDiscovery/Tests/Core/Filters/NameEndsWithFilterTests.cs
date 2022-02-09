/*
 * Copyright © 2022 DotNotStandard. All rights reserved.
 * 
 * See the LICENSE file in the root of the repo for licensing details.
 * 
 */
using DotNotStandard.DependencyInjection.AutoDiscovery.Filters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNotStandard.DependencyInjection.AutoDiscovery.UnitTests.Filters
{

	[TestClass]
	public class NameEndsWithFilterTests
	{

		#region Constructor

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Type_InitializedWithNull_ThrowsArgumentNullException()
		{
			// Arrange
			IFilter classUnderTest = new NameEndsWithFilter(null);
		}

		#endregion

		#region Matches

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Matches_Null_ThrowsArgumentNullException()
		{
			// Arrange
			IFilter classUnderTest = new NameEndsWithFilter("Match");
			bool expected = false;
			bool actual;

			// Act
			actual = classUnderTest.Matches(null);

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void Matches_FakeViewModelEndsWithEmptyString_ReturnsTrue()
		{
			// Arrange
			IFilter classUnderTest = new NameEndsWithFilter("");
			bool expected = true;
			bool actual;

			// Act
			actual = classUnderTest.Matches(typeof(Fakes.ViewModels.FakeViewModel));

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void Matches_FakeViewModelEndsWithBlah_ReturnsFalse()
		{
			// Arrange
			IFilter classUnderTest = new NameEndsWithFilter("Blah");
			bool expected = false;
			bool actual;

			// Act
			actual = classUnderTest.Matches(typeof(Fakes.ViewModels.FakeViewModel));

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void Matches_FakeViewModelEndsWithFake_ReturnsFalse()
		{
			// Arrange
			IFilter classUnderTest = new NameEndsWithFilter("Fake");
			bool expected = false;
			bool actual;

			// Act
			actual = classUnderTest.Matches(typeof(Fakes.ViewModels.FakeViewModel));

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void Matches_FakeViewModelEndsWithViewModel_ReturnsTrue()
		{
			// Arrange
			IFilter classUnderTest = new NameEndsWithFilter("ViewModel");
			bool expected = true;
			bool actual;

			// Act
			actual = classUnderTest.Matches(typeof(Fakes.ViewModels.FakeViewModel));

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void Matches_FakeViewModelEndsWithLowercaseViewModel_ReturnsTrue()
		{
			// Arrange
			IFilter classUnderTest = new NameEndsWithFilter("viewmodel");
			bool expected = true;
			bool actual;

			// Act
			actual = classUnderTest.Matches(typeof(Fakes.ViewModels.FakeViewModel));

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void Matches_FakeViewModelEndsWithUppercaseViewModel_ReturnsTrue()
		{
			// Arrange
			IFilter classUnderTest = new NameEndsWithFilter("VIEWMODEL");
			bool expected = true;
			bool actual;

			// Act
			actual = classUnderTest.Matches(typeof(Fakes.ViewModels.FakeViewModel));

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void Matches_FakeViewModelEndsWithUppercaseViewModelWithOrdinalComparison_ReturnsFalse()
		{
			// Arrange
			IFilter classUnderTest = new NameEndsWithFilter("VIEWMODEL", StringComparison.Ordinal);
			bool expected = false;
			bool actual;

			// Act
			actual = classUnderTest.Matches(typeof(Fakes.ViewModels.FakeViewModel));

			// Assert
			Assert.AreEqual(expected, actual);

		}

		#endregion

		#region Private Helper Methods

		#endregion
	}

}