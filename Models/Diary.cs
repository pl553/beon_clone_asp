using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Beon.Models {
  public class Diary : Board {
    //required relationship
    public string OwnerId { get; set; } = null!;
    public BeonUser? Owner { get; set; }
  }
}