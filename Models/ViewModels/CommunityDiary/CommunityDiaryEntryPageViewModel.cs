namespace Beon.Models.ViewModels
{
  public record CommunityDiaryEntryPageViewModel(
    CommunityDiaryEntryPageViewModel Page,
    DiaryEntryViewModel Entry) : CommunityDiaryPageViewModel(Page)
  {
    //hide
    private CommunityDiaryEntryPageViewModel Page { get; init; } = default!;
  }
}