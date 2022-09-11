using Beon.Models;
using Beon.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace Beon.Services {
  public class BoardLogic {
    private readonly UserManager<BeonUser> _userManager;
    private readonly PostLogic _postLogic;
    private readonly IRepository<Post> _postRepository;
    private readonly IRepository<Topic> _topicRepository;
    private readonly IRepository<Board> _boardRepository;
    private readonly TopicLogic _topicLogic;
    private readonly LinkGenerator _linkGenerator;
    private readonly ILogger<TopicLogic> _logger;
    public BoardLogic(
      PostLogic postLogic,
      UserManager<BeonUser> userManager,
      IRepository<Post> postRepository,
      IRepository<Topic> topicRepository,
      IRepository<Board> boardRepository,
      ILogger<TopicLogic> logger,
      LinkGenerator linkGenerator,
      TopicLogic topicLogic)
    {
      _postLogic = postLogic;
      _postRepository = postRepository;
      _boardRepository = boardRepository;
      _topicRepository = topicRepository;
      _linkGenerator = linkGenerator;
      _userManager = userManager;
      _logger = logger;
      _topicLogic = topicLogic;
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
        Post? op = await _postRepository.Entities
          .Where(p => p.TopicId.Equals(t.TopicId))
          .Include(p => p.Poster)
          .FirstOrDefaultAsync();
        
        if (op == null) {
          _logger.LogWarning($"Topic with id {t.TopicId} has no op");
          continue;
        }

        bool canEdit = await _topicLogic.UserMayEditTopicAsync(t, user);
        int postCount = await _postRepository.Entities
          .Where(p => p.TopicId.Equals(t.TopicId))
          .CountAsync();
        
        res.Add(new TopicPreviewViewModel(
          t.TopicId,
          await _topicLogic.GetTopicPathAsync(t),
          t.Title,
          await _postLogic.GetPostViewModelAsync(op),
          postCount,
          t.TimeStamp,
          canEdit));
      }

      return res;
    }
  }
}