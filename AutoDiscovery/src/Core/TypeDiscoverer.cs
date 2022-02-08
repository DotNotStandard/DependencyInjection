using DotNotStandard.DependencyInjection.AutoDiscovery.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNotStandard.DependencyInjection.AutoDiscovery
{
	internal class TypeDiscoverer
	{

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

		private bool ShouldBeExcluded(TypeDiscoveryOptions discoveryOptions, Type possibleMatch)
		{
			foreach (IFilter exclusion in discoveryOptions.Exclusions)
			{
				if (exclusion.Matches(possibleMatch)) return true;
			}

			// Doesn't match any of the exclusions, so it is not excluded
			return false;
		}

		private bool ShouldBeIncluded(TypeDiscoveryOptions discoveryOptions, Type possibleMatch)
		{
			foreach (IFilter inclusion in discoveryOptions.Inclusions)
			{
				if (inclusion.Matches(possibleMatch)) return true;
			}

			// Doesn't match any of the inclusions, so it is not included
			return false;
		}

		#endregion

	}
}
