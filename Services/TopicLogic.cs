using Beon.Models;
using Beon.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace Beon.Services {
  public class TopicLogic {
    private readonly UserManager<BeonUser> _userManager;
    private readonly IRepository<Post> _postRepository;
    private readonly IRepository<Topic> _topicRepository;
    private readonly IRepository<Board> _boardRepository;
    private readonly LinkGenerator _linkGenerator;
    private readonly ILogger<TopicLogic> _logger;
    public TopicLogic(
      UserManager<BeonUser> userManager,
      IRepository<Post> postRepository,
      IRepository<Topic> topicRepository,
      IRepository<Board> boardRepository,
      ILogger<TopicLogic> logger,
      LinkGenerator linkGenerator)
    {
      _postRepository = postRepository;
      _boardRepository = boardRepository;
      _topicRepository = topicRepository;
      _linkGenerator = linkGenerator;
      _userManager = userManager;
      _logger = logger;
    }
    public async Task<bool> TopicWithIdExistsAsync(int topicId) =>
      await _topicRepository.Entities.Where(t => t.TopicId.Equals(topicId)).CountAsync() > 0;
    public async Task<string> GetTopicPathAsync(Topic topic) {
      Board board = await LoadBoardAsync(topic);
      string? res = null;
      if (board.Type == BoardType.Diary) {
        res = _linkGenerator.GetPathByAction("Show", "DiaryTopic", new { userName = board.OwnerName, topicOrd = topic.TopicOrd });
      }
      else throw new Exception("unsupported boardtype");
      if (res == null) throw new Exception("could'nt generate topic path");
      return res;
    }

    public async Task<bool> UserMayEditTopicAsync(Topic topic, BeonUser? user) {
      if (user == null) {
        return false;
      }
      Board board = await LoadBoardAsync(topic);
      if (board.Type == BoardType.Diary) {
        return user.UserName.Equals(board.OwnerName);
      }
      else throw new Exception("unsupported boardtype");
    }

    public async Task<ICollection<int>> GetPostIdsOfTopicAsync(int topicId) {
      return await _postRepository.Entities
        .Where(p => p.TopicId.Equals(topicId))
        .Select(p => p.PostId)
        .ToListAsync();
    }

    public async Task<LinkViewModel> GetShortLinkAsync(Topic topic){
      string text = topic.Title.Substring(0, Math.Min(34, topic.Title.Length));
      if (topic.Title.Length > 34) {
        text += "...";
      }
      return new LinkViewModel(text, await GetTopicPathAsync(topic));
    } 
    public async Task<int> GetNumPagesAsync(Expression<Func<Topic,bool>> which) {
      int n = Beon.Settings.Page.ItemCount;
      return (await _topicRepository.Entities.Where(which).CountAsync() + n - 1) / n;
    }
    public async Task<ICollection<TopicPreviewViewModel>> GetTopicPreviewViewModelsAsync(
      Expression<Func<Topic,bool>> which,
      int page,
      ClaimsPrincipal userClaim)
    {
      var topics = await _topicRepository.Entities
        .Where(which)
        .OrderByDescending(t => t.TopicId)
        .Skip((page-1)*Beon.Settings.Page.ItemCount)
        .Take(Beon.Settings.Page.ItemCount)
        .Include(t => t.Board)
        .ToListAsync();
      
      BeonUser? user = await _userManager.GetUserAsync(userClaim);

      List<TopicPreviewViewModel> res = new List<TopicPreviewViewModel>();
      foreach(var t in topics) {
        int opId = await _postRepository.Entities
          .Where(p => p.TopicId.Equals(t.TopicId))
          .Select(p => p.PostId)
          .FirstOrDefaultAsync();
        
        if (opId == 0) {
          _logger.LogWarning($"Topic with id {t.TopicId} has no op");
          continue;
        }

        bool canEdit = await UserMayEditTopicAsync(t, user);
        int postCount = await _postRepository.Entities
          .Where(p => p.TopicId.Equals(t.TopicId))
          .CountAsync();
        
        res.Add(new TopicPreviewViewModel(
          t.TopicId,
          await GetTopicPathAsync(t),
          t.Title,
          opId,
          postCount,
          t.TimeStamp,
          canEdit));
      }

      return res;
    }
    private async Task<Board> LoadBoardAsync(Topic topic) {
      if (topic.Board == null) {
        topic.Board = await _boardRepository.Entities
          .Where(b => b.BoardId.Equals(topic.BoardId))
          .FirstOrDefaultAsync();
      }
      if (topic.Board == null) {
        throw new Exception("invalid topic");
      }
      return topic.Board;
    }
  }
}