using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Beon.Models.ViewModels {
  public class PostCreateViewModel {
    public int TopicId { get; set; }
    public PostCreateViewModel(int topicId) {
      TopicId = topicId;
    }
  }
}