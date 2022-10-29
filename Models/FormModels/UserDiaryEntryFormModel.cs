using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Beon.Models
{
  public class UserDiaryEntryFormModel : TopicFormModel
  {
    [Required]
    public string DiaryOwnerUserName { get; set; } = "";

    [Required]
    public UserDiaryEntry.Access ReadAccess { get; set; }
    [Required]
    public UserDiaryEntry.Access CommentAccess { get; set; }

    public List<SelectListItem> AccessValues { get; } = new List<SelectListItem>()
    {
      new SelectListItem
      {
        Value = UserDiaryEntry.Access.Everyone.ToString(),
        Text = "Все"
      },
      new SelectListItem
      {
        Value = UserDiaryEntry.Access.Users.ToString(),
        Text = "Пользователи"
      },
      new SelectListItem
      {
        Value = UserDiaryEntry.Access.Friends.ToString(),
        Text = "Друзья"
      },
      new SelectListItem
      {
        Value = UserDiaryEntry.Access.NoOne.ToString(),
        Text = "Никто"
      }
    };
  }
}