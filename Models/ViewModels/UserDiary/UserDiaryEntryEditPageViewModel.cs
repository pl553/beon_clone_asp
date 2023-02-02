namespace Beon.Models.ViewModels
{
  public record UserDiaryEntryEditPageViewModel (
    string UserName,
    UserDiaryEntryFormModel Entry
  );
}