using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using Beon.Models;
using Beon.Models.ViewModels;

namespace Beon.Services
{
  public class UserDiaryEntryService
  {
    private readonly BeonDbContext _context;
    private readonly UserManager<BeonUser> _userManager;
    private readonly IRepository<UserDiaryEntry> _userDiaryEntryRepository;

    public UserDiaryEntryService(
      BeonDbContext context,
      IRepository<UserDiaryEntry> userDiaryEntryRepository,
      UserManager<BeonUser> userManager)
    {
      _context = context;
      _userManager = userManager;
      _userDiaryEntryRepository = userDiaryEntryRepository;
    }

    public async Task<UserDiaryEntry> CreateFromAsync(UserDiaryEntryFormModel model, BeonUser? user)
    {
      var diaryOwner = await _userManager.Users
        .Where(u => u.UserName == model.DiaryOwnerUserName)
        .Include(u => u.Diary)
        .FirstOrDefaultAsync();

      if (diaryOwner == null || user == null || diaryOwner.Id != user.Id)
      {
        throw new Exception();
      }

      var entry = new UserDiaryEntry(
        context: _context,
        body: model.Body,
        timeStamp: DateTime.UtcNow,
        posterId: user.Id,
        title: model.Title,
        topicOrd: await (await diaryOwner.GetDiaryAsync()).GetNextTopicOrdAsync(),
        userDiaryId: (await diaryOwner.GetDiaryAsync()).DiaryId,
        readAccess: UserDiaryEntry.Access.Everyone,
        commentAccess: UserDiaryEntry.Access.Users,
        desires: "",
        mood: "",
        music: "");

      await _userDiaryEntryRepository.CreateAsync(entry);

      return entry;
    }
  }
}