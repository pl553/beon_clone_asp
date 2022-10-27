using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Beon.Models {
  public class Diary
  {
    public int DiaryId { get; set; }
    public string Title { get; set; } = "";
    public string Subtitle { get; set; } = "";
    //styling?
  }
}