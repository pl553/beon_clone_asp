using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Beon.Models {
  public class PublicForum {
    public int PublicForumId { get; set; }
    public int BoardId { get; set; }
    public Board? Board { get; set; }
    public string? Name { get; set; }
  }
}