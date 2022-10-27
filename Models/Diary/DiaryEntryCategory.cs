namespace Beon.Models
{
  public class DiaryEntryCategory
  {
    public int DiaryEntryCategoryId { get; set; }
    public int DiaryId { get; set; }
    public Diary? Diary { get; set; }
    public int DiaryEntryPostId { get; set; }
    public DiaryEntry? DiaryEntry { get; set; }
    //required
    public string Category { get; set; } = null!;
  }
}