using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Beon.Models.ViewModels {
  public class CommentCreateViewModel {
    public int TopicId { get; set; }
    public CommentCreateViewModel(int topicId) {
      TopicId = topicId;
    }
  }
}