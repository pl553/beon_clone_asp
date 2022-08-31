using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using SixLabors.ImageSharp;
using Beon.Infrastructure;

namespace Beon.Validation {
    public class ImageAttribute : ValidationAttribute, IClientModelValidator
    {
        private int _maxWidth;
        private int _maxHeight;
        public ImageAttribute(int maxWidth, int maxHeight) {
          _maxWidth = maxWidth;
          _maxHeight = maxHeight;
        }

        private string GetMaxWidthErrorMessage() {
          return $"Maximum allowed width is {_maxWidth} pixels.";
        }

        private string GetMaxHeightErrorMessage() {
          return $"Maximum allowed height is {_maxHeight} pixels.";  
        }

        private string GetInvalidImageErrorMessage() {
          return "The image is corrupted or in an unrecognizable format.";
        }

        protected override ValidationResult? IsValid(
        object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                using (var stream = new MemoryStream()) {
                  file.CopyTo(stream);
                  
                  try {
                    using (var image = Image.Load(stream.ToArray())) {
                      if (image.Width > _maxWidth) {
                        return new ValidationResult(GetMaxWidthErrorMessage());
                      }
                      if (image.Height > _maxHeight) {
                        return new ValidationResult(GetMaxHeightErrorMessage());
                      }
                    }
                  }
                  catch {
                    return new ValidationResult(GetInvalidImageErrorMessage());
                  }

                }
            }

            return ValidationResult.Success;
        }

        public void AddValidation(ClientModelValidationContext context) {
            context.MergeAttribute("data-val", "true");
            context.MergeAttribute("data-val-image", GetInvalidImageErrorMessage());
            context.MergeAttribute("data-val-image-width", GetMaxWidthErrorMessage());
            context.MergeAttribute("data-val-image-width-max", _maxWidth.ToString());
            context.MergeAttribute("data-val-image-height", GetMaxHeightErrorMessage());
            context.MergeAttribute("data-val-image-height-max", _maxHeight.ToString());
        }
    }
}
