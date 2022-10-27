namespace Beon.Models.ViewModels
{
  public record DiaryEntryPreviewViewModel(
    TopicPreviewViewModel Topic,
    IEnumerable<string> Categories) : TopicPreviewViewModel(Topic)
  {
    //hide
    private TopicPreviewViewModel Topic { get; init; } = default!;

    public static async Task<DiaryEntryPreviewViewModel> CreateFromAsync(
      DiaryEntry entry, BeonUser? user)
    {
      var t = await TopicPreviewViewModel.CreateFromAsync(entry, user);
      if (!t.CanRead)
      {
        return new DiaryEntryPreviewViewModel(t, Enumerable.Empty<string>());
      }
      else
      {
        return new DiaryEntryPreviewViewModel(t, await entry.GetCategoriesAsync());
      }
    }
  }
}