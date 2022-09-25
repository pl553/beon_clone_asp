using Beon.Models;
using Beon.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace Beon.Services
{
  public class FriendLogic
  {
    private readonly UserManager<BeonUser> _userManager;
    private readonly IRepository<FriendLink> _friendLinkRepository;

    public FriendLogic(
      UserManager<BeonUser> userManager,
      IRepository<FriendLink> friendLinkRepository)
    {
      _userManager = userManager;
      _friendLinkRepository = friendLinkRepository;
    }

    public async Task CreateAsync(BeonUser from, BeonUser to)
    {
      if (from.Id == to.Id || await IsUserFriendOfAsync(from, to))
      {
        return;
      }
      FriendLink link = new FriendLink
      {
        From = from,
        To = to
      };
      await _friendLinkRepository.CreateAsync(link);
    }

    public async Task DeleteAsync(BeonUser from, BeonUser to)
    {
      FriendLink? link = await _friendLinkRepository.Entities
        .Where(f => f.FromId == from.Id && f.ToId == to.Id)
        .FirstOrDefaultAsync();

      if (link != null)
      {
        await _friendLinkRepository.DeleteAsync(link);
      }
    }

    public async Task<bool> AreUsersMutualsAsync(BeonUser a, BeonUser b)
      => await IsUserFriendOfAsync(a, b) && await IsUserFriendOfAsync(b, a);

    public async Task<bool> IsUserFriendOfAsync(string fromId, string toId)
    {
      int cnt = await _friendLinkRepository.Entities
        .Where(f => f.FromId == fromId && f.ToId == toId)
        .CountAsync();

      return cnt > 0;
    }
    /// <summary>
    /// Determines if user "to" is in the friend list of "from" (if "from" has sent a friend req to "to")
    /// </summary>
    public async Task<bool> IsUserFriendOfAsync(BeonUser from, BeonUser to)
      => await IsUserFriendOfAsync(from.Id, to.Id);

    /// <summary>
    /// Get users that "user" has sent a friend req to
    /// </summary>
    public async Task<IEnumerable<BeonUser>> GetFriendsAsync(BeonUser user)
      => (await _friendLinkRepository.Entities
        .Where(f => f.FromId == user.Id)
        .Include(link => link.To)
        .Select(link => link.To)
        .ToListAsync())
        .NotNull();

    /// <summary>
    /// Get users that have "user" in their friend lists (have sent a friend req to "user")
    /// </summary>
    public async Task<IEnumerable<BeonUser>> GetUsersThatFriendedUserAsync(BeonUser user)
      => (await _friendLinkRepository.Entities
        .Where(f => f.ToId == user.Id)
        .Include(link => link.From)
        .Select(link => link.From)
        .ToListAsync())
        .NotNull();

    public async Task<IEnumerable<BeonUser>> GetMutualsAsync(BeonUser user)
    {
      var a = await GetFriendsAsync(user);
      var b = await GetUsersThatFriendedUserAsync(user);
      return a.Intersect(b);
    }
  }
}