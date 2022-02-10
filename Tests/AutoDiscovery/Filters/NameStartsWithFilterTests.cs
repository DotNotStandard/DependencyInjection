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
	public class NameStartsWithFilterTests
	{

		#region Constructor

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Type_InitializedWithNull_ThrowsArgumentNullException()
		{
			// Arrange
			IFilter classUnderTest = new NameStartsWithFilter(null);
		}

		#endregion

		#region Matches

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Matches_Null_ThrowsArgumentNullException()
		{
			// Arrange
			IFilter classUnderTest = new NameStartsWithFilter("Match");
			bool expected = false;
			bool actual;

			// Act
			actual = classUnderTest.Matches(null);

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void Matches_FakeViewModelStartsWithEmptyString_ReturnsTrue()
		{
			// Arrange
			IFilter classUnderTest = new NameStartsWithFilter("");
			bool expected = true;
			bool actual;

			// Act
			actual = classUnderTest.Matches(typeof(Fakes.ViewModels.FakeViewModel));

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void Matches_FakeViewModelStartsWithBlah_ReturnsFalse()
		{
			// Arrange
			IFilter classUnderTest = new NameStartsWithFilter("Blah");
			bool expected = false;
			bool actual;

			// Act
			actual = classUnderTest.Matches(typeof(Fakes.ViewModels.FakeViewModel));

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void Matches_FakeViewModelStartsWithViewModel_ReturnsFalse()
		{
			// Arrange
			IFilter classUnderTest = new NameStartsWithFilter("ViewModel");
			bool expected = false;
			bool actual;

			// Act
			actual = classUnderTest.Matches(typeof(Fakes.ViewModels.FakeViewModel));

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void Matches_FakeViewModelStartsWithFake_ReturnsTrue()
		{
			// Arrange
			IFilter classUnderTest = new NameStartsWithFilter("Fake");
			bool expected = true;
			bool actual;

			// Act
			actual = classUnderTest.Matches(typeof(Fakes.ViewModels.FakeViewModel));

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void Matches_FakeViewModelStartsWithLowercaseFake_ReturnsTrue()
		{
			// Arrange
			IFilter classUnderTest = new NameStartsWithFilter("fake");
			bool expected = true;
			bool actual;

			// Act
			actual = classUnderTest.Matches(typeof(Fakes.ViewModels.FakeViewModel));

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void Matches_FakeViewModelStartsWithUppercaseFake_ReturnsTrue()
		{
			// Arrange
			IFilter classUnderTest = new NameStartsWithFilter("FAKE");
			bool expected = true;
			bool actual;

			// Act
			actual = classUnderTest.Matches(typeof(Fakes.ViewModels.FakeViewModel));

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void Matches_FakeViewModelStartsWithUppercaseFakeWithOrdinalComparison_ReturnsFalse()
		{
			// Arrange
			IFilter classUnderTest = new NameStartsWithFilter("FAKE", StringComparison.Ordinal);
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