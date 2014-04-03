using System;
using System.Collections.Generic;
using Slim.Models;

namespace Slim.ViewModels
{
	public class ShortUrlListViewModel
	{
		public IEnumerable<ShortUrl> SlimUrls { get; set; }
	}
}