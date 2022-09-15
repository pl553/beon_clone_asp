using Beon.Models;
using Beon.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace Beon.Services {
  public class TopicLogic {
    private readonly UserManager<BeonUser> _userManager;
    private readonly IRepository<OriginalPost> _opRepository;
    private readonly IRepository<Comment> _commentRepository;
    private readonly IRepository<Topic> _topicRepository;
    private readonly IRepository<Board> _boardRepository;
    private readonly LinkGenerator _linkGenerator;
    private readonly ILogger<TopicLogic> _logger;
    private readonly PostLogic _postLogic;
    public TopicLogic(
      PostLogic postLogic,
      UserManager<BeonUser> userManager,
      IRepository<OriginalPost> opRepository,
      IRepository<Comment> commentRepository,
      IRepository<Topic> topicRepository,
      IRepository<Board> boardRepository,
      ILogger<TopicLogic> logger,
      LinkGenerator linkGenerator)
    {
      _postLogic = postLogic;
      _opRepository = opRepository;
      _commentRepository = commentRepository;
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


    public async Task<PostViewModel> GetOpAsync(int topicId)
    {
      var post = await _opRepository.Entities
        .Where(p => p.TopicId.Equals(topicId))
        .FirstOrDefaultAsync();

      if (post == null)
      {
        throw new Exception("topic has no op");
      }

      return await _postLogic.GetPostViewModelAsync(post);
    }
    public async Task<ICollection<CommentViewModel>> GetCommentsAsync(int topicId, BeonUser? user) {
      var comments = await _commentRepository.Entities
        .Where(p => p.TopicId.Equals(topicId))
        .Include(p => p.Poster)
        .ToListAsync();
      
      return await Task.WhenAll(comments.Select(async p => await _postLogic.GetCommentViewModelAsync(p, user)).ToList());
    }
    public async Task<LinkViewModel> GetShortLinkAsync(Topic topic){
      string text = topic.Title.Substring(0, Math.Min(34, topic.Title.Length));
      if (topic.Title.Length > 34) {
        text += "...";
      }
      return new LinkViewModel(text, await GetTopicPathAsync(topic));
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