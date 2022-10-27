namespace Beon.Models.ViewModels {
  public record PosterViewModel(
    string UserName,
    string DisplayName,
    string? AvatarFilePath
  )
  {
    public const PosterViewModel? Anonymous = null;

    public static async Task<PosterViewModel?> CreateFromAsync(BeonUser? user)
    {
      if (user == BeonUser.Anonymous)
      {
        return Anonymous;
      }
      else
      {
        return new PosterViewModel(
          user.UserName,
          user.DisplayName,
          await user.GetAvatarUrlAsync());
      }
    }
  }
}