using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Beon.Models {
  public class Board {
    public int BoardId { get; set; }
    //set by EF
    //see https://learn.microsoft.com/en-us/ef/core/modeling/inheritance
    public string Discriminator { get; set; } = null!;
    public ICollection<Topic> Topics { get; set; } = new List<Topic>();
    public int TopicCounter { get; set; } = 0;
  }
}