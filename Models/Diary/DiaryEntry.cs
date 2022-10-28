using Microsoft.EntityFrameworkCore;

namespace Beon.Models
{
  public abstract class DiaryEntry : Topic
  {
    private IRepository<DiaryEntryCategory> _diaryEntryCategoryRepository;

    protected DiaryEntry(
      BeonDbContext context,
      string body,
      DateTime timeStamp,
      string? posterId,
      string title,
      int topicOrd) : base(context, body, timeStamp, posterId, title, topicOrd)
    {
      _diaryEntryCategoryRepository = context.GetRequiredService<IRepository<DiaryEntryCategory>>();
    }

    public async Task<IEnumerable<string>> GetCategoriesAsync()
    {
      return await _diaryEntryCategoryRepository.Entities
        .Where(dc => dc.DiaryEntryPostId == PostId)
        .Select(dc => dc.Category)
        .ToListAsync();
    }
  }
}