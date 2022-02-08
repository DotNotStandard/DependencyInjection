using System;
using System.Collections.Generic;
using System.Text;

namespace DotNotStandard.DependencyInjection.AutoDiscovery.Filters
{
	internal class FuncFilter : IFilter
	{
		private readonly Func<Type, bool> _filterExpression;

		public FuncFilter(Func<Type, bool> filterExpression)
		{
			_filterExpression = filterExpression;
		}

		public bool Matches(Type type)
		{
			return _filterExpression(type);
		}
	}
}
