using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Beon.Models
{
  public class Post
  {
    [BindNever]
    public int PostId { get; set; }

    [BindNever]
    public Topic? Topic { get; set; }

    [Required]
    [DataType(DataType.Text)]
    public string? Body { get; set; }

    [BindNever]
    public DateTime TimeStamp { get; set; }

    [BindNever]
    public BeonUser? Poster { get; set; }
  }
}