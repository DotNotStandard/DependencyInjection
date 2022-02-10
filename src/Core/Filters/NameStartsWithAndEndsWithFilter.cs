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
	/// IFilter implementation that matches on types by naming, with a specified prefix and suffix
	/// </summary>
	public class NameStartsWithAndEndsWithFilter : IFilter
	{
		private readonly string _startMatch;
		private readonly string _endMatch;
		private readonly StringComparison _stringComparison;

		#region Constructors

		public NameStartsWithAndEndsWithFilter(string startMatch, string endMatch)
		{
			if (startMatch is null) throw new ArgumentNullException(nameof(startMatch));
			if (endMatch is null) throw new ArgumentNullException(nameof(endMatch));

			_startMatch = startMatch;
			_endMatch = endMatch;
			_stringComparison = StringComparison.CurrentCultureIgnoreCase;
		}

		public NameStartsWithAndEndsWithFilter(string startMatch, string endMatch, StringComparison stringComparison)
		{
			if (startMatch is null) throw new ArgumentNullException(nameof(startMatch));
			if (endMatch is null) throw new ArgumentNullException(nameof(endMatch));

			_startMatch = startMatch;
			_endMatch = endMatch;
			_stringComparison = stringComparison;
		}

		#endregion

		/// <inheritdoc cref="IFilter"/>
		public bool Matches(Type type)
		{
			if (type is null) throw new ArgumentNullException(nameof(type));
			return type.Name.StartsWith(_startMatch, _stringComparison) && 
				type.Name.EndsWith(_endMatch, _stringComparison);
		}
	}
}
