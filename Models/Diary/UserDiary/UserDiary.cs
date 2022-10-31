using Microsoft.EntityFrameworkCore;

namespace Beon.Models
{
  public class UserDiary : Diary
  {
    public string OwnerId { get; set; }
    public BeonUser? Owner { get; set; }
    public ICollection<UserDiaryEntry> Entries { get; set; } = new List<UserDiaryEntry>();

    private readonly BeonDbContext _context;

    private readonly LinkGenerator _linkGenerator;

    public UserDiary() { }
    public UserDiary(BeonDbContext context)
    {
      _context = context;
      _linkGenerator = _context.GetRequiredService<LinkGenerator>();
    }

    public async Task<int> GetNextTopicOrdAsync()
    {
      var cnt = await _context.Entry(this)
        .Collection(d => d.Entries)
        .Query()
        .CountAsync();

      return cnt == 0 ? 0
        : await _context.Entry(this)
            .Collection(d => d.Entries)
            .Query()
            .Select(e => e.TopicOrd)
            .MaxAsync() + 1;
    }

    public async Task<string> GetPathAsync(int page = 1)
    => _linkGenerator.GetPathByAction(
      "Show",
      "UserDiary",
      new
      {
        userName = (await GetOwnerAsync()).UserName,
        page = page
      }) ?? throw new Exception("couldn't generate user diary path");

    public async Task<BeonUser> GetOwnerAsync()
    {
      await _context.Entry(this).Reference(d => d.Owner).LoadAsync();
      return Owner ?? throw new Exception("invalid diary: has no owner");
    }
  }
}