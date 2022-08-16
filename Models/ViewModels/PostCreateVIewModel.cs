using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Beon.Models.ViewModels {
  public class PostCreateViewModel {
    public Post Post { get; set; } = new Post();
    [BindNever]
    public BoardType BoardType { get; set; }
    [BindNever]
    public string BoardOwnerName { get; set; } = String.Empty;
    public int TopicId { get; set; }
  }
}