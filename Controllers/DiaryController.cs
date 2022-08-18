using Microsoft.AspNetCore.Mvc;
using Beon.Models;
using Beon.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Beon.Controllers
{
  public class DiaryController : Controller
  {
    private readonly ILogger _logger;
    private readonly UserManager<BeonUser> _userManager;
    private readonly ITopicRepository _topicRepository;
    private readonly IPostRepository _postRepository;
    private readonly IBoardRepository _boardRepository;
    public DiaryController(
      UserManager<BeonUser> userManager,
      ITopicRepository topicRepository,
      IBoardRepository boardRepository,
      IPostRepository postRepository,
      ILogger<DiaryController> logger) {
      _userManager = userManager;
      _topicRepository = topicRepository;
      _boardRepository = boardRepository;
      _postRepository = postRepository;
      _logger = logger;
    }

    [Route("/diary/{userName:required}")]
    public async Task<IActionResult> Show (string userName)
    {
      string? displayName = await _userManager.Users
        .Where(u => u.UserName.Equals(userName))
        .Select(u => u.DisplayName)
        .FirstOrDefaultAsync();
      
      if (displayName == null) {
        return NotFound();
      }
      
      Board? b = await _boardRepository.Boards
        .Where(b => b.OwnerName.Equals(userName) && b.Type.Equals(BoardType.Diary))
        .Include(b => b.Topics)
        .FirstOrDefaultAsync();

      if (b == null) {
        return NotFound();
      }

      ICollection<TopicPreviewViewModel> previews = new List<TopicPreviewViewModel>();

      int i = 1;
      foreach (var t in b.Topics) {
        Post? op = await _postRepository.Posts
          .Where(p => p.TopicId.Equals(t.TopicId))
          .Include(p => p.Poster)
          .FirstOrDefaultAsync();
        
        if (op != null && op.Poster != null) {
          PosterViewModel posterVm = new PosterViewModel(op.Poster.UserName, op.Poster.DisplayName);
          PostShowViewModel opVm = new PostShowViewModel(op.Body, op.TimeStamp, posterVm);
          previews.Add(new TopicPreviewViewModel(b.Type, b.OwnerName, i, t.Title, opVm));
        }
        ++i;
      }
      ViewBag.IsDiaryPage = true;
      ViewBag.DiaryTitle = displayName;
      ViewBag.DiarySubtitle = displayName;
      return View(new DiaryViewModel(new BoardShowViewModel(b.Type, b.OwnerName, previews)));
    }
  }
}