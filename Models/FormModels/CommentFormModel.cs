using System.ComponentModel.DataAnnotations;

namespace Beon.Models
{
  public class CommentFormModel : PostFormModel
  {
    [Required]
    public int TopicPostId { get; set; }
  }
}