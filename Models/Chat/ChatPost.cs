using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Beon.Models
{
  public class ChatPost : Post
  {
    public int ChatId { get; set; }
    public virtual Chat Chat { get; set; } = null!;

    public async override Task<bool> UserCanReadAsync(BeonUser? user)
    {
      throw new NotImplementedException();
    }
  }
}