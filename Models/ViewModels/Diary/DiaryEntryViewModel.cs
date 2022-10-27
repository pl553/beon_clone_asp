namespace Beon.Models.ViewModels {
  public record DiaryEntryViewModel(
    TopicViewModel Topic,
    IEnumerable<string> Categories) : TopicViewModel(Topic)
  {
    //hide
    private TopicViewModel Topic { get; init; } = default!;

    public static async Task<DiaryEntryViewModel> CreateFromAsync(
      DiaryEntry entry, BeonUser? user)
    => new DiaryEntryViewModel(
      await TopicViewModel.CreateFromAsync(entry, user),
      await entry.GetCategoriesAsync());
  }
}