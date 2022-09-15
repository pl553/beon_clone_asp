using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Beon.Models {
  public class Topic {
    [BindNever]
    public int TopicId { get; set; }
    [BindNever]
    public int TopicOrd { get; set; }
    [BindNever]
    public int BoardId { get; set; }
    [BindNever]
    public Board? Board { get; set; }
    public string Title { get; set; } = String.Empty;
    public DateTime TimeStamp { get; set; }
    public string? PosterId { get; set; }
    public BeonUser? Poster { get; set; }
    public int OriginalPostId { get; set; }
    public OriginalPost? OriginalPost { get; set; }
    public ICollection<Comment>? Comments { get; set; }
  }
}