using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Beon.Models.ViewModels {
  public class TopicCreateViewModel {
    public string CreatePath { get; set; }
    public TopicCreateViewModel(string createPath) {
      CreatePath = createPath;
    }
  }
}