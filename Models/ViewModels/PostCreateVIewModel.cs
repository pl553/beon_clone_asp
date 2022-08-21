using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Beon.Models.ViewModels {
  public class PostCreateViewModel {
    public string CreatePath { get; set; }
    public PostCreateViewModel(string createPath) {
      CreatePath = createPath;
    }
  }
}