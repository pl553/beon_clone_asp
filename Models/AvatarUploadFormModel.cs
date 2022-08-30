using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Beon.Models
{
  public class AvatarUploadFormModel
  {
    [Required]
    public IFormFile File { get; set; } = null!;
  }
}