using System;
using System.ComponentModel.DataAnnotations;

namespace Slim
{
	public abstract class AbstractUrlAttribute : ValidationAttribute
	{
		protected ValidationResult CreateValidationResultError(ValidationContext context)
		{
			return new ValidationResult(this.FormatErrorMessage(context.DisplayName));
		}

		protected Uri GetAbsoluteUri(string value)
		{
			if (String.IsNullOrEmpty(value)) {
				return null;
			}

			if (String.IsNullOrWhiteSpace(value)) {
				return null;
			}

			Uri uri;

			if (Uri.TryCreate(value.ToString(), UriKind.Absolute, out uri) == false) {
				return null;
			}

			return uri;
		}

	}
}