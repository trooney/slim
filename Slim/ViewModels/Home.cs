using System;
using System.Collections.Generic;

using Slim.Models;

namespace Slim.ViewModels
{
	public class Home
	{
		public ShortUrlCreate NewShortUrl { get; set; }
		public IEnumerable<ShortUrl> RecentShortUrls { get; set; }

		public Home()
		{
			this.NewShortUrl = new ShortUrlCreate();
		}
	}
}