namespace Beon.Models.ViewModels
{
  public record PublicForumTopicsPageViewModel(
    string Name,
    IEnumerable<TopicPreviewViewModel> Topics);
}