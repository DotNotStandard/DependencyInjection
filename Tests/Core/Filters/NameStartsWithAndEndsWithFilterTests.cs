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
	public class NameStartsWithAndEndsWithFilterTests
	{

		#region Constructor

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Type_InitializedWithStartNull_ThrowsArgumentNullException()
		{
			// Arrange
			IFilter classUnderTest = new NameStartsWithAndEndsWithFilter(null, "Test");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Type_InitializedWithEndNull_ThrowsArgumentNullException()
		{
			// Arrange
			IFilter classUnderTest = new NameStartsWithAndEndsWithFilter("Test", null);
		}

		#endregion

		#region Matches

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Matches_Null_ThrowsArgumentNullException()
		{
			// Arrange
			IFilter classUnderTest = new NameStartsWithAndEndsWithFilter("Start", "End");
			bool expected = false;
			bool actual;

			// Act
			actual = classUnderTest.Matches(null);

			// Assert
			Assert.AreEqual(expected, actual);

		}

		#region Starts With

		[TestMethod]
		public void Matches_FakeViewModelStartsWithEmptyString_ReturnsTrue()
		{
			// Arrange
			IFilter classUnderTest = new NameStartsWithAndEndsWithFilter("", "");
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
			IFilter classUnderTest = new NameStartsWithAndEndsWithFilter("Blah", "");
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
			IFilter classUnderTest = new NameStartsWithAndEndsWithFilter("ViewModel", "");
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
			IFilter classUnderTest = new NameStartsWithAndEndsWithFilter("Fake", "");
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
			IFilter classUnderTest = new NameStartsWithAndEndsWithFilter("fake", "");
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
			IFilter classUnderTest = new NameStartsWithAndEndsWithFilter("FAKE", "");
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
			IFilter classUnderTest = new NameStartsWithAndEndsWithFilter("FAKE", "", StringComparison.Ordinal);
			bool expected = false;
			bool actual;

			// Act
			actual = classUnderTest.Matches(typeof(Fakes.ViewModels.FakeViewModel));

			// Assert
			Assert.AreEqual(expected, actual);

		}

		#endregion

		#region Ends With

		[TestMethod]
		public void Matches_FakeViewModelEndsWithEmptyString_ReturnsTrue()
		{
			// Arrange
			IFilter classUnderTest = new NameStartsWithAndEndsWithFilter("", "");
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
			IFilter classUnderTest = new NameStartsWithAndEndsWithFilter("", "Blah");
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
			IFilter classUnderTest = new NameStartsWithAndEndsWithFilter("", "Fake");
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
			IFilter classUnderTest = new NameStartsWithAndEndsWithFilter("", "ViewModel");
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
			IFilter classUnderTest = new NameStartsWithAndEndsWithFilter("", "viewmodel");
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
			IFilter classUnderTest = new NameStartsWithAndEndsWithFilter("", "VIEWMODEL");
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
			IFilter classUnderTest = new NameStartsWithAndEndsWithFilter("", "VIEWMODEL", StringComparison.Ordinal);
			bool expected = false;
			bool actual;

			// Act
			actual = classUnderTest.Matches(typeof(Fakes.ViewModels.FakeViewModel));

			// Assert
			Assert.AreEqual(expected, actual);

		}

		#endregion

		#endregion

		#region Private Helper Methods

		#endregion
	}

}