using System.ComponentModel.DataAnnotations;

namespace Beon.Models
{
  public class PostFormModel
  {
    [Required]
    [DataType(DataType.Text)]
    public string Body { get; set; } = "";
  }
}