/*
 * Copyright © 2022 DotNotStandard. All rights reserved.
 * 
 * See the LICENSE file in the root of the repo for licensing details.
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNotStandard.DependencyInjection.AutoDiscovery.UnitTests.Fakes.Repositories
{
	internal class FakeIdAndNameRepository : IFakeIdAndNameRepository
	{
		public Task<IdAndName> Fetch(int id)
		{
			return Task.FromResult(new IdAndName());
		}
	}
}
