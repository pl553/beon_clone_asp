namespace Beon.Models.ViewModels
{
  public record CommunityDiaryEntryPreviewViewModel(
    DiaryEntryPreviewViewModel DiaryEntry,
    CommunityLinkViewModel Link) : DiaryEntryPreviewViewModel(DiaryEntry)
  {
    //hide
    private DiaryEntryPreviewViewModel DiaryEntry { get; init; } = default!;
  }
}