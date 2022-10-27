namespace Beon.Models.ViewModels
{
  public record CommunityDiaryEntriesPageViewModel(
    CommunityDiaryPageViewModel Page,
    IEnumerable<DiaryEntryPreviewViewModel> Entries) : CommunityDiaryPageViewModel(Page)
  {
    //hide
    private CommunityDiaryPageViewModel Page { get; init; } = default!;
  }
}