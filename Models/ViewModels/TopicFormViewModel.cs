using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Beon.Models.ViewModels {
  public class TopicFormViewModel {
    public Topic Topic { get; set; } = new Topic();
    public int boardId { get; set; }
  }
}