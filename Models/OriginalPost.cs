using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Beon.Models
{
  public class OriginalPost : Post
  {
    public int TopicId { get; set; }
    public Topic? Topic { get; set; }
  }
}