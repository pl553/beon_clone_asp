using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Beon.Models
{
  public class TopicFormModel : PostFormModel
  {
    [Required(ErrorMessage = "Please enter a title")]
    [MaxLength(64)]
    [DataType(DataType.Text)]
    public string Title { get; set; } = "";
  }
}