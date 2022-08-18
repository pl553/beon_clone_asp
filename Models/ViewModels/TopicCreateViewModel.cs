using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Beon.Models.ViewModels {
  public class TopicCreateViewModel {
    public BoardType BoardType { get; set; }
    public string BoardOwnerName { get; set; }
    public TopicCreateViewModel(BoardType boardType, string boardOwnerName) {
      BoardType = boardType;
      BoardOwnerName = boardOwnerName;
    }
  }
}