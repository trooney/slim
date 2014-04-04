using System;
using System.Collections.Generic;
using Slim.Models;

namespace Slim.ViewModels
{
	public class ShortUrlListViewModel
	{
		public IEnumerable<ShortUrl> ShortUrls { get; set; }
	}
}