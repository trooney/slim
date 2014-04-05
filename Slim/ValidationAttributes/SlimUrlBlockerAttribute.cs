using System;
using System.ComponentModel.DataAnnotations;

namespace Slim
{
	public class SlimUrlBlockerAttribute : AbstractUrlAttribute
	{

		public override bool IsValid(object value)
		{
			Uri uri = GetAbsoluteUri((string)value);

			if (uri == null) {
				return false;
			}

			if (uri.Host.IndexOf("slimurl.org", 0, StringComparison.CurrentCultureIgnoreCase) != -1) {
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

//		private ValidationResult CreateValidationResultError(ValidationContext context)
//		{
//			return new ValidationResult(this.FormatErrorMessage(context.DisplayName));
//		}
//
//		protected override ValidationResult IsValid(object value, ValidationContext context)
//		{
//			Uri uri = GetAbsoluteUri((string)value);
//
//			if (uri == null) {
//				return CreateValidationResultError(context);
//			}
//
//			if (uri.Host.IndexOf("slimurl.org", 0, StringComparison.CurrentCultureIgnoreCase) != -1) {
//				return CreateValidationResultError(context);
//			}
//
//			return ValidationResult.Success;
//		}
	}
}