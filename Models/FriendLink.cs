using Microsoft.EntityFrameworkCore;

namespace Beon.Models
{
  /// <summary>
  /// friend request
  /// </summary>
  [Index(nameof(FromId))]
  [Index(nameof(ToId))]
  public class FriendLink
  {
    public int FriendLinkId { get; set; }
    public string FromId { get; set; } = null!;
    public BeonUser? From { get; set; }
    public string ToId { get; set; } = null!;
    public BeonUser? To { get; set; }
  }
}