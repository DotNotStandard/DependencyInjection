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
	/// IFilter implementation that uses an expression to perform filtering
	/// </summary>
	internal class ExpressionFilter : IFilter
	{
		private readonly Func<Type, bool> _filterExpression;

		#region Constructors

		public ExpressionFilter(Func<Type, bool> filterExpression)
		{
			_filterExpression = filterExpression;
		}

		#endregion

		/// <inheritdoc cref="IFilter"/>
		public bool Matches(Type type)
		{
			return _filterExpression(type);
		}
	}
}
