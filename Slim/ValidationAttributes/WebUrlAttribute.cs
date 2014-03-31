using System;
using System.ComponentModel.DataAnnotations;


namespace Slim
{
	public class WebUrlAttribute : ValidationAttribute
	{
		private ValidationResult CreateValidationResultError(ValidationContext context)
		{
			return new ValidationResult(this.FormatErrorMessage(context.DisplayName));
		}
	
		protected override ValidationResult IsValid(object value, ValidationContext context)
		{
			if (value == null) {
				return CreateValidationResultError(context);
			}

			Uri uri;
				
			if (Uri.TryCreate(value.ToString(), UriKind.Absolute, out uri) == false) {
				return CreateValidationResultError(context);
			}

			if (uri.Scheme == "http" || uri.Scheme == "https") {
				return ValidationResult.Success;
			}

			return CreateValidationResultError(context);
		}
	}
}

