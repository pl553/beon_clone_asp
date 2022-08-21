using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Beon.Models
{
  public class TopicFormModel
  {
    [Required(ErrorMessage = "Please enter a title")]
    [MaxLength(30)]
    [DataType(DataType.Text)]
    public string Title { get; set; } = String.Empty;
    public PostFormModel Op { get; set; } = new PostFormModel();
  }
}