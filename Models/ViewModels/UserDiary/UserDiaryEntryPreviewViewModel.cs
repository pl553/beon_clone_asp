namespace Beon.Models.ViewModels
{
  public record UserDiaryEntryPreviewViewModel(
    DiaryEntryPreviewViewModel DiaryEntry,
    string Desires,
    string Mood,
    string Music) : DiaryEntryPreviewViewModel(DiaryEntry)
  {
    //hide
    private DiaryEntryPreviewViewModel DiaryEntry { get; init; } = default!;

    public static async Task<UserDiaryEntryPreviewViewModel> CreateFromAsync(
      UserDiaryEntry entry, BeonUser? user)
    {
      var de = await DiaryEntryPreviewViewModel.CreateFromAsync(entry, user);
      if (!de.CanRead)
      {
        return new UserDiaryEntryPreviewViewModel(
          de,
          Desires: "",
          Mood: "",
          Music: "");
      }
      else
      {
        return new UserDiaryEntryPreviewViewModel(
          de,
          entry.Desires,
          entry.Mood,
          entry.Music);
      }
    }
  }
}