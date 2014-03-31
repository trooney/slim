using System;
using System.ComponentModel.DataAnnotations;

namespace Slim.ViewModels
{
	public class SlimUrlCreateViewModel
	{
		[Required(ErrorMessage = "Enter a URL")]
		[MinLength(3)]
		[MaxLength(2048)]
		[WebUrl(ErrorMessage = "Enter a full URL")]
		public string FullUrl { get; set; }
	}
}

