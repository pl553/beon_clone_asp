using Beon.Models;

namespace Beon.Models.ViewModels
{
  public record UserDiaryViewModel(
    string UserName,
    bool CanCreateEntries,
    IEnumerable<UserDiaryEntryPreviewViewModel> Entries);
}