using System;
using System.ComponentModel.DataAnnotations;

namespace Slim.ViewModels
{
	public class ShortUrlCreate
	{
		[Required(ErrorMessage = "Enter a URL")]
		[MinLength(3)]
		[MaxLength(2048)]
		[WebUrl]
		[SlimUrlBlocker]
		public string FullUrl { get; set; }
	}
}

