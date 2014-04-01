using System;

using Slim.Components;

namespace Slim.Services
{
	public abstract class Service
	{
		public DependencyManager dm;

		public Service (DependencyManager dm)
		{
			this.dm = dm;
		}
	}
}

