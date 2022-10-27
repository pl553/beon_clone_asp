using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Beon.Models
{
  public class UserDiaryEntryFormModel : TopicFormModel
  {
    [Required]
    public string DiaryOwnerUserName { get; set; } = "";
  }
}