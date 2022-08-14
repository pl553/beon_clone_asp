using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Beon.Models {
  public class Board {
    [BindNever]
    public int BoardId { get; set; }

    [Required(ErrorMessage = "Please enter a name")]
    [MaxLength(30)]
    public string Name { get; set; } = String.Empty;

    [BindNever]
    public ICollection<Topic> Topics { get; set; } = new List<Topic>();
  }
}