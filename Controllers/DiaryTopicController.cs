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
    private LinkGenerator _linkGenerator;
    private readonly ILogger _logger;
    private readonly ITopicSubscriptionRepository _tsRepository;
    public DiaryTopicController(
      ITopicRepository repo,
      IBoardRepository boardRepo,
      IPostRepository postRepo,
      UserManager<BeonUser> userManager,
      LinkGenerator linkGenerator,
      ILogger<DiaryTopicController> logger,
      ITopicSubscriptionRepository tsRepository) {
      repository = repo;
      boardRepository = boardRepo;
      postRepository = postRepo;
      _userManager = userManager;
      _logger = logger;
      _linkGenerator = linkGenerator;
      _tsRepository = tsRepository;
    }

    [HttpPost]
    [Authorize]
    [Route("/diary/{userName:required}/CreateTopic")]
    public async Task<IActionResult> Create(string userName, TopicFormModel model) {
      string loggedInUserName = await _userManager.GetUserNameAsync(await _userManager.GetUserAsync(User));

      if (loggedInUserName != userName) {
        return NotFound();
      }
      
      Board? board = await boardRepository.Boards
        .Where(b => b.OwnerName.Equals(userName) && b.Type.Equals(BoardType.Diary))
        .FirstOrDefaultAsync();

      if (board == null) {
        return NotFound();
      }
      
      BeonUser? u = await _userManager.GetUserAsync(User);

      if (u == null) {
        return NotFound();
      }

      if (ModelState.IsValid) {
        int topicOrd = board.topicCounter++;
        boardRepository.UpdateBoard(board);
        DateTime timeStamp = DateTime.UtcNow;
        Topic topic = new Topic { Title = model.Title, BoardId = board.BoardId, TopicOrd = topicOrd, TimeStamp = timeStamp };
        repository.SaveTopic(topic);
        Post op = new Post { Body = model.Op.Body, Topic = topic, Poster = u, TimeStamp = timeStamp };
        postRepository.SavePost(op);
        await _tsRepository.SubscribeAsync(topic.TopicId, u.Id);
        return RedirectToAction("Show", "DiaryTopic", new { userName = userName, topicOrd = topicOrd});
      }
      else {
        return NotFound();
      }
    }

    [Route("/diary/{userName:required}/0-{topicOrd:int}")]
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
        .Where(t => t.BoardId.Equals(boardId) && t.TopicOrd.Equals(topicOrd))
        .FirstOrDefaultAsync();

      if (t == null) {
        return NotFound();
      }

      string? postCreatePath = _linkGenerator.GetPathByAction("Create", "DiaryPost", new { userName = userName, topicOrd = topicOrd });  

      if (postCreatePath == null) {
        throw new Exception("Couldn't generate post creation path");
      }

      BeonUser? user = await _userManager.GetUserAsync(User);
      if (user != null) {
        await _tsRepository.UnsetNewCommentsAsync(t.TopicId, user.Id);
      }

      ICollection<int> postIds = await postRepository.GetPostIdsOfTopicAsync(t.TopicId);

      ViewBag.IsDiaryPage = true;
      ViewBag.DiaryTitle = displayName;
      ViewBag.DiarySubtitle = displayName;
      return View(new DiaryTopicShowViewModel(userName, new TopicShowViewModel(postCreatePath, t.TopicId, t.Title, postIds)));
    }
  }
}