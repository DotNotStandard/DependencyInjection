/*
 * Copyright © 2022 DotNotStandard. All rights reserved.
 * 
 * See the LICENSE file in the root of the repo for licensing details.
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNotStandard.DependencyInjection.AutoDiscovery.Filters
{
	/// <summary>
	/// Contract defining the behaviour of a type filter
	/// </summary>
	public interface IFilter
	{

		/// <summary>
		/// Check if a type matches against the rules of the filter
		/// </summary>
		/// <param name="type">The type under test</param>
		/// <returns>True if the type matches the filter, otherwise false</returns>
		bool Matches(Type type);

	}
}
