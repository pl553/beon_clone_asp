using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Beon.Models
{
  public class Post
  {
    public int PostId { get; set; }
    public string Body { get; set; } = String.Empty;
    public DateTime TimeStamp { get; set; }
    public string PosterId { get; set; } = null!;
    public BeonUser? Poster { get; set; }
  }
}