using System;
using System.Collections;
using System.Collections.Generic;

using Slim.Models;

namespace Slim.ViewModels
{
	public class Shorten
	{
		public ShortUrlCreate NewShortUrl { get; set; }

		public List<ShortUrl> LastShortUrls { get; set; }

		public IEnumerable<ShortUrl> RecentShortUrls { get; set; }

		public Shorten()
		{
			this.NewShortUrl = new ShortUrlCreate();
			this.LastShortUrls = new List<ShortUrl>();
		}
	}
}

