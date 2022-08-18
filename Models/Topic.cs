using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Beon.Models {
  public class Topic {
    [BindNever]
    public int TopicId { get; set; }
    [BindNever]
    public int BoardId { get; set; }
    [BindNever]
    public Board? Board { get; set; }
    [Required(ErrorMessage = "Please enter a title")]
    [MaxLength(30)]
    public string Title { get; set; } = String.Empty;

    public ICollection<Post> Posts { get; set; } = new List<Post>();
  }
}