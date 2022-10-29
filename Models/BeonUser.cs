using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Beon.Models;
using Beon.Models.ViewModels;
using Beon.Services;

namespace Beon.Models {
  public class BeonUser : IdentityUser {
    public const BeonUser? Anonymous = null;
    public const string? AnonymousId = null;
    public string DisplayName { get; set; } = "default";
    public string? AvatarFileName { get; set; } = null;
    public UserDiary? Diary { get; set; }

    private readonly IUserFileRepository _userFileRepository;

    private readonly IRepository<FriendLink> _friendLinkRepository;

    private readonly BeonDbContext _context;
    
    public BeonUser() { }

    private BeonUser(BeonDbContext context)
    {
      _userFileRepository = context.GetRequiredService<IUserFileRepository>();
      _friendLinkRepository = context.GetRequiredService<IRepository<FriendLink>>();
      _context = context;
    }

    public async Task<string?> GetAvatarUrlAsync()
      => AvatarFileName == null ? null
        : await _userFileRepository.GetFileUrlAsync(UserName, AvatarFileName);

    public async Task<bool> IsFriendsWithAsync(BeonUser? user)
      => user != BeonUser.Anonymous
        && (user.Id == Id || await _friendLinkRepository.Entities
          .Where(fl => fl.FromId == Id && fl.ToId == user.Id).CountAsync() > 0);

    public async Task SendFriendRequest(BeonUser to)
    {
      if (await IsFriendsWithAsync(to))
      {
        return;
      }
      var fl = new FriendLink
      {
        FromId = Id,
        ToId = to.Id
      };
      await _friendLinkRepository.CreateAsync(fl);
    }

    public async Task RevokeFriendRequest(BeonUser to)
    {
      var fl = await _friendLinkRepository.Entities
        .Where(fl => fl.FromId == Id && fl.ToId == to.Id)
        .FirstOrDefaultAsync();

      if (fl != null)
      {
        await _friendLinkRepository.DeleteAsync(fl);
      }
    }

    public async Task<IEnumerable<BeonUser>> GetFriendsAsync()
    => await _friendLinkRepository.Entities
      .Where(fl => fl.FromId == Id)
      .Include(fl => fl.To)
      .Select(fl => fl.To!)
      .ToListAsync();

    public async Task<IEnumerable<BeonUser>> GetUsersThatHaveMeInTheirFriendListAsync()
    => await _friendLinkRepository.Entities
      .Where(fl => fl.ToId == Id)
      .Include(fl => fl.From)
      .Select(fl => fl.From!)
      .ToListAsync();

    public async Task<IEnumerable<BeonUser>> GetMutualsAsync()
    => (await GetFriendsAsync()).Intersect(await GetUsersThatHaveMeInTheirFriendListAsync());

    public async Task<UserDiary> GetDiaryAsync()
    {
      await _context.Entry(this).Reference(u => u.Diary).LoadAsync();
      return Diary ?? throw new Exception("invalid user: has no diary");
    }


  }
}