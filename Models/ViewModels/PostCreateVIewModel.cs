using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Beon.Models.ViewModels {
  public class PostCreateViewModel {
    public BoardType BoardType { get; set; }
    public string BoardOwnerName { get; set; }
    public int TopicOrd { get; set; }
    public PostCreateViewModel(
      BoardType boardType,
      string boardOwnerName,
      int topicOrd) {
      BoardType = boardType;
      BoardOwnerName = boardOwnerName;
      TopicOrd = topicOrd;
    }
  }
}