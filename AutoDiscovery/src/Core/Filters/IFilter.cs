using System;
using System.Collections.Generic;
using System.Text;

namespace DotNotStandard.DependencyInjection.AutoDiscovery.Filters
{
	public interface IFilter
	{

		bool Matches(Type type);

	}
}
