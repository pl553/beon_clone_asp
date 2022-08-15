using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Beon.Models {
  public enum BoardType
  {
    Diary,
    Community,
    PublicForum
  }

  public class Board {
    [BindNever]
    public int BoardId { get; set; }

    [Required(ErrorMessage = "Please enter a name")]
    [MaxLength(30)]
    public string Name { get; set; } = String.Empty;

    [BindNever]
    public ICollection<Topic> Topics { get; set; } = new List<Topic>();

    [BindNever]
    public BoardType Type { get; set; } = BoardType.Diary;

    //username of diary owner if the type is diary
    //communityname if the type is community
    //forum name if the type is f0rum 
    [BindNever]
    public string OwnerName { get; set; } = String.Empty;
  }
}