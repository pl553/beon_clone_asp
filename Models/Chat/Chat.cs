namespace Beon.Models
{
  public class Chat
  {
    public int ChatId { get; set; }
    public virtual ICollection<ChatPost> Posts { get; set; } = new List<ChatPost>();
  }
}