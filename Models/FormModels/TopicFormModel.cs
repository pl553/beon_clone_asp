using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Beon.Infrastructure;

namespace Beon.Models
{
  public class TopicFormModel : PostFormModel
  {
    [MaxLength(Settings.Topic.MaxTitleLength)]
    [DataType(DataType.Text)]
    public string? Title { get; set; }
  }
}