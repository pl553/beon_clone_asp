using Beon.Models.ViewModels;

namespace Beon.Models
{
  public class UserDiaryEntry : DiaryEntry
  {
    public int UserDiaryId { get; set; }
    public UserDiary? UserDiary { get; set; }

    public enum Access
    {
      Invalid = 0,
      Everyone,
      Users,
      Friends,
      NoOne
    }

    public Access ReadAccess { get; set; }
    public Access CommentAccess { get; set; }

    public string Desires { get; set; } = "";
    public string Mood { get; set; } = "";
    public string Music { get; set; } = "";

    public UserDiaryEntry(
      BeonDbContext context,
      string body,
      DateTime timeStamp,
      string? posterId,
      string title,
      int topicOrd,
      int userDiaryId,
      Access readAccess,
      Access commentAccess,
      string desires,
      string mood,
      string music) : base(context, body, timeStamp, posterId, title, topicOrd)
    {
      UserDiaryId = userDiaryId;
      ReadAccess = readAccess;
      CommentAccess = commentAccess;
      Desires = desires;
      Mood = mood;
      Music = music;
    }

    public override string CannotCommentReason
    {
      get => CommentAccess switch
      {
        UserDiaryEntry.Access.Everyone => ":O",
        UserDiaryEntry.Access.NoOne => "Комментировать может только автор.",
        UserDiaryEntry.Access.Friends => "Комментировать могут только друзья.",
        UserDiaryEntry.Access.Users => "Комментировать могут только пользователи.",
        _ => throw new ArgumentOutOfRangeException(nameof(CommentAccess))
      };
    }

    public override string CannotReadReason
    {
      get => ReadAccess switch
      {
        UserDiaryEntry.Access.Everyone => ":O",
        UserDiaryEntry.Access.NoOne => "Запись только для меня.",
        UserDiaryEntry.Access.Friends => "Запись только для друзей.",
        UserDiaryEntry.Access.Users => "Запись только для пользователей.",
        _ => throw new ArgumentOutOfRangeException(nameof(ReadAccess))
      };
    }

    public override async Task<TopicPreviewViewModel> CreateTopicPreviewViewModelAsync(BeonUser? user)
      => await UserDiaryEntryPreviewViewModel.CreateFromAsync(this, user);

    public override async Task<string> GetPathAsync()
      => _linkGenerator.GetPathByAction(
        "Show",
        "UserDiaryEntry",
        new
        {
          userName = (await (await GetUserDiaryAsync()).GetOwnerAsync()).UserName,
          topicOrd = TopicOrd,
        }) ?? throw new Exception("couldn't generate diary entry path");
    
    public override async Task<bool> UserCanCommentAsync(BeonUser? user)
      => await UserCanAccessAsync(CommentAccess, user);

    public override async Task<bool> UserCanReadAsync(BeonUser? user)
      => await UserCanAccessAsync(ReadAccess, user);
    
    private async Task<bool> UserCanAccessAsync(
      UserDiaryEntry.Access access,
      BeonUser? user)
    => access switch
      {
        UserDiaryEntry.Access.NoOne => user != BeonUser.Anonymous && PosterId == user.Id,
        UserDiaryEntry.Access.Everyone => true,
        UserDiaryEntry.Access.Friends
          => !Anonymous && await (await GetNonAnonymousPosterAsync()).IsFriendsWithAsync(user),
        UserDiaryEntry.Access.Users => user != BeonUser.Anonymous,
        _ => throw new ArgumentOutOfRangeException(nameof(access))
      };

    public override Task<bool> UserModeratesAsync(BeonUser? user)
      => Task.FromResult(user != BeonUser.Anonymous && user.Id == PosterId);

    public async Task<UserDiary> GetUserDiaryAsync()
    {
      await _context.Entry(this).Reference(e => e.UserDiary).LoadAsync();
      return UserDiary ?? throw new Exception("Invalid user diary entry: not attached to a user diary");
    }
  }
}