/*
 * Copyright © 2022 DotNotStandard. All rights reserved.
 * 
 * See the LICENSE file in the root of the repo for licensing details.
 * 
 */
using DotNotStandard.DependencyInjection.AutoDiscovery.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNotStandard.DependencyInjection.AutoDiscovery
{

	/// <summary>
	/// Performs type discovery against a predefined list of rules
	/// </summary>
	internal class TypeDiscoverer
	{

		/// <summary>
		/// Find types that match the inclusion and exclusion list from the appropriate assembly
		/// </summary>
		/// <param name="discoveryOptions">The options with which to perform discovery</param>
		/// <returns>An IEnumerable of all of the types that met the requested expectations</returns>
		public IEnumerable<Type> FindMatchingTypes(TypeDiscoveryOptions discoveryOptions)
		{
			IList<Type> discoveredTypes = new List<Type>();

			foreach (Type possibleMatch in discoveryOptions.TargetAssembly.GetTypes())
			{
				if (ShouldBeExcluded(discoveryOptions, possibleMatch)) continue;
				if (ShouldBeIncluded(discoveryOptions, possibleMatch))
				{
					discoveredTypes.Add(possibleMatch);
				}
			}

			return discoveredTypes;
		}

		#region Private Helper Methods

		/// <summary>
		/// Run the exclusion rules against a type to determine if this type should be excluded
		/// from the results that the class will return
		/// </summary>
		/// <param name="discoveryOptions">The options with which discovery is being performed</param>
		/// <param name="possibleMatch">The type to check against the exclusions</param>
		/// <returns>Boolean; true if the type is to be excluded, otherwise false</returns>
		private bool ShouldBeExcluded(TypeDiscoveryOptions discoveryOptions, Type possibleMatch)
		{
			foreach (IFilter exclusion in discoveryOptions.Exclusions)
			{
				if (exclusion.Matches(possibleMatch)) return true;
			}

			// Doesn't match any of the exclusions, so it is not to be excluded
			return false;
		}

		/// <summary>
		/// Run the inclusion rules against a type to determine if this type should be included
		/// in the results that the class will return
		/// </summary>
		/// <param name="discoveryOptions">The options with which discovery is being performed</param>
		/// <param name="possibleMatch">The type to check against the inclusions</param>
		/// <returns>Boolean; true if the type is to be included, otherwise false</returns>
		private bool ShouldBeIncluded(TypeDiscoveryOptions discoveryOptions, Type possibleMatch)
		{
			foreach (IFilter inclusion in discoveryOptions.Inclusions)
			{
				if (inclusion.Matches(possibleMatch)) return true;
			}

			// Doesn't match any of the inclusions, so it is not to be included
			return false;
		}

		#endregion

	}
}
