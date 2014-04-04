using System;

namespace Slim.ViewModels
{
	public class HomeViewModel
	{
		public ShortUrlCreateViewModel CreateViewModel { get; set; }
		public ShortUrlListViewModel ListViewModel { get; set; }

		public HomeViewModel()
		{
			this.CreateViewModel = new ShortUrlCreateViewModel();
			this.ListViewModel = new ShortUrlListViewModel();
		}
	}
}