using System;

namespace Slim
{
	public interface IShortUrl
	{
		string FullUrl { get; set; }
		string Hash { get; set; }
	}
}

