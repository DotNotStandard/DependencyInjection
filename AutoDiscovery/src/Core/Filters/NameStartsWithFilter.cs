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
	/// IFilter implementation that matches on types by naming, with a specified prefix
	/// </summary>
	public class NameStartsWithFilter : IFilter
	{
		private readonly string _nameMatch;
		private readonly StringComparison _stringComparison;

		#region Constructors

		public NameStartsWithFilter(string nameMatch)
		{
			if (nameMatch is null) throw new ArgumentNullException(nameof(nameMatch));
			
			_nameMatch = nameMatch;
			_stringComparison = StringComparison.CurrentCultureIgnoreCase;
		}

		public NameStartsWithFilter(string nameMatch, StringComparison stringComparison)
		{
			if (nameMatch is null) throw new ArgumentNullException(nameof(nameMatch));

			_nameMatch = nameMatch;
			_stringComparison = stringComparison;
		}

		#endregion

		/// <inheritdoc cref="IFilter"/>
		public bool Matches(Type type)
		{
			if (type is null) throw new ArgumentNullException(nameof(type));
			return type.Name.StartsWith(_nameMatch, _stringComparison);
		}
	}
}
