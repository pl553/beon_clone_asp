using Microsoft.AspNetCore.Mvc;
using Beon.Models;
using Beon.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Beon.Infrastructure;

namespace Beon.Controllers
{
  public class DiaryTopicController : Controller
  {
    private readonly UserManager<BeonUser> _userManager;
    private ITopicRepository repository;
    private IBoardRepository boardRepository;
    private IPostRepository postRepository;
    private readonly ILogger _logger;
    public DiaryTopicController(
      ITopicRepository repo,
      IBoardRepository boardRepo,
      IPostRepository postRepo,
      UserManager<BeonUser> userManager,
      ILogger<DiaryTopicController> logger) {
      repository = repo;
      boardRepository = boardRepo;
      postRepository = postRepo;
      _userManager = userManager;
      _logger = logger;
    }

    [HttpPost]
    [Authorize]
    [Route("/diary/{userName:required}/CreateTopic")]
    public async Task<IActionResult> Create(string userName, TopicFormModel model) {
      int boardId = await boardRepository.Boards
        .Where(b => b.OwnerName.Equals(userName) && b.Type.Equals(BoardType.Diary))
        .Select(b => b.BoardId)
        .FirstOrDefaultAsync();

      if (boardId == 0) {
        return NotFound();
      }
      
      BeonUser? u = await _userManager.GetUserAsync(User);

      if (u == null) {
        return NotFound();
      }

      if (ModelState.IsValid) {
        int topicCount = repository.Topics
          .Where(t => t.BoardId.Equals(boardId))
          .Count();

        Topic topic = new Topic { Title = model.Title, BoardId = boardId };
        repository.SaveTopic(topic);
        Post op = new Post { Body = model.Op.Body, Topic = topic, Poster = u, TimeStamp = DateTime.UtcNow };
        postRepository.SavePost(op);
        return RedirectToAction("Show", "DiaryTopic", new { userName = userName, topicOrd = topicCount+1});
      }
      else {
        return NotFound();
      }
    }

    [Route("/diary/{userName:required}/{topicOrd:int}")]
    public async Task<IActionResult> Show(string userName, int topicOrd) {
      string? displayName = await _userManager.Users
        .Where(u => u.UserName.Equals(userName))
        .Select(u => u.DisplayName)
        .FirstOrDefaultAsync();
      
      if (displayName == null) {
        return NotFound();
      }

      int boardId = await boardRepository.Boards
        .Where(b => b.OwnerName.Equals(userName) && b.Type.Equals(BoardType.Diary))
        .Select(b => b.BoardId)
        .FirstOrDefaultAsync();
      
      Topic? t = await repository.Topics
        .Where(t => t.BoardId.Equals(boardId))
        .Skip(topicOrd-1)
        .Include(t => t.Posts)
        .ThenInclude(p => p.Poster)
        .FirstOrDefaultAsync();

      if (t == null) {
        return NotFound();
      }

      ICollection<PostShowViewModel> posts = new List<PostShowViewModel>();

      foreach (var p in t.Posts) if (p.Poster != null) {
        posts.Add(new PostShowViewModel(p.Body, p.TimeStamp, new PosterViewModel(p.Poster.UserName, p.Poster.DisplayName), true));
      }
      
      ViewBag.IsDiaryPage = true;
      ViewBag.DiaryTitle = displayName;
      ViewBag.DiarySubtitle = displayName;
      return View(new TopicShowViewModel(BoardType.Diary, userName, topicOrd, t.Title, posts));
    }
  }
}