using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Beon.Validation;

namespace Beon.Models
{
  public class AvatarUploadFormModel
  {
    [Required]
    [MaxFileSize(1 * 1024 * 1024)]
    public IFormFile File { get; set; } = null!;
  }
}