using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Beon.Infrastructure {
  public static class ClientModelValidationContextExtensions {
    public static bool MergeAttribute(this ClientModelValidationContext context, string key, string value) {
      return context.Attributes.TryAdd(key, value);
    }
  }
}