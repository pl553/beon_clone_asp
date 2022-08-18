using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Beon.Models
{
  public class PostFormModel
  {
    [Required]
    [DataType(DataType.Text)]
    public string Body { get; set; } = String.Empty;
  }
}