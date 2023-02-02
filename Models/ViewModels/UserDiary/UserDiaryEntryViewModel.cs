namespace Beon.Models.ViewModels
{
  public record UserDiaryEntryViewModel(
    DiaryEntryViewModel DiaryEntry,
    string Desires,
    string Mood,
    string Music
  ) : DiaryEntryViewModel(DiaryEntry)
  
  {
    //hide
    private DiaryEntryViewModel DiaryEntry { get; init; } = default!;

    public static async Task<UserDiaryEntryViewModel> CreateFromAsync(
      UserDiaryEntry entry,
      BeonUser? user
    )
    => new UserDiaryEntryViewModel(
      await DiaryEntryViewModel.CreateFromAsync(entry, user),
      entry.Desires,
      entry.Mood,
      entry.Music
    );
  }
}