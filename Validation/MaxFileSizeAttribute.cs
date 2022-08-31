using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Beon.Infrastructure;

namespace Beon.Validation {
    public class MaxFileSizeAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        public string GetErrorMessage()
        {
            return $"Maximum allowed file size is { _maxFileSize} bytes.";
        }
        protected override ValidationResult? IsValid(
        object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
            if (file.Length > _maxFileSize)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public void AddValidation(ClientModelValidationContext context) {
            context.MergeAttribute("data-val", "true");
            context.MergeAttribute("data-val-filesize", GetErrorMessage());
            context.MergeAttribute("data-val-filesize-max", _maxFileSize.ToString());
        }
    }
}
