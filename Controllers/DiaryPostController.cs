using Microsoft.AspNetCore.Mvc;
using Beon.Models;
using Beon.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Beon.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Beon.Controllers
{
  public class DiaryPostController : Controller
  {
    private IBoardRepository _boardRepository;
    private IPostRepository repository;
    private ITopicRepository _topicRepository;
    private readonly UserManager<BeonUser> _userManager;    
    private readonly ILogger _logger;
    public DiaryPostController(
      IBoardRepository boardRepository,
      IPostRepository repo,
      ITopicRepository TopicRepo,
      UserManager<BeonUser> userManager,
      ILogger<DiaryPostController> logger) {
      repository = repo;
      _boardRepository = boardRepository;
      _topicRepository = TopicRepo;
      _userManager = userManager;
      _logger = logger;
    }
    public IActionResult Index() {
      return View();
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    [Route("/diary/{userName:required}/{topicOrd:int}/CreatePost")]
    public async Task<IActionResult> Create(string userName, int topicOrd, PostFormModel model) {
      if (!ModelState.IsValid) {
        return NotFound();
      }

      int boardId = await _boardRepository.Boards
        .Where(b => b.OwnerName.Equals(userName) && b.Type.Equals(BoardType.Diary))
        .Select(b => b.BoardId)
        .FirstOrDefaultAsync();
      
      if (boardId == 0) {
        return NotFound();
      }

      int topicId = await _topicRepository.Topics
        .Where(t => t.BoardId.Equals(boardId))
        .Skip(topicOrd-1)
        .Select(t => t.TopicId)
        .FirstOrDefaultAsync();

      if (topicId == 0) {
        return NotFound();
      }
      
      BeonUser? u = await _userManager.Users
        .Where(u => u.UserName.Equals(userName))
        .FirstOrDefaultAsync();   
         
      if (u == null) {
        return NotFound();
      }

      Post p = new Post { TopicId = topicId, Body = model.Body, TimeStamp = DateTime.UtcNow, Poster = u };
      repository.SavePost(p);
      return RedirectToAction("Show", "DiaryTopic", new { topicOrd = topicOrd, userName = userName });
    }
  }
}