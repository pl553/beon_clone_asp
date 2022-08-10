using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Beon.Models.ViewModels {
  public class PostCreateViewModel {
    public Post Post { get; set; } = new Post();
    public int TopicId { get; set; }
  }
}