using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Beon.Models {
  public class Diary {
    public int DiaryId { get; set; }
    public Board? Board { get; set; }
    public string? OwnerId { get; set; }
    public BeonUser? Owner { get; set; }
  }
}