using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Beon.Models
{
  public class Post
  {
    public int PostId { get; set; }
    public int? TopicId { get; set; }
    public Topic? Topic { get; set; }
    [DataType(DataType.Text)]
    public string Body { get; set; } = String.Empty;
    public DateTime TimeStamp { get; set; }
    public string? PosterId { get; set; }
    public BeonUser? Poster { get; set; }
  }
}