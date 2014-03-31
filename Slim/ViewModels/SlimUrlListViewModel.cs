using System;
using System.Collections.Generic;
using Slim.Models;

namespace Slim.ViewModels
{
	public class SlimUrlListViewModel
	{
		public IEnumerable<SlimUrl> SlimUrls { get; set; }
	}
}