using System;
using System.ComponentModel.DataAnnotations;

namespace Slim
{
	public class WebUrlAttribute : AbstractUrlAttribute
	{

		public override bool IsValid(object value)
		{
			Uri uri = GetAbsoluteUri((string)value);

			if (uri == null) {
				return false;
			}

			// make sure host contains both hostname and tld
			// i.e., filter out things like "localhost"
			if (uri.Host.Contains(".") == false) {
				return false;
			}

			// only accept http|https
			if (false == (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)) {
				return false;
			}

			return true;
		}
	
		protected override ValidationResult IsValid(object value, ValidationContext context)
		{
			return IsValid(value) 
				? ValidationResult.Success 
				: new ValidationResult(this.FormatErrorMessage(context.DisplayName));
		}
	}
}