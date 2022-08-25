using Microsoft.AspNetCore.Mvc;
using Beon.Models;
using Beon.Hubs;
using Beon.Models.ViewModels;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Beon.Infrastructure;
using System.Security.Claims;

namespace Beon.Components {
  public class NewCommentsNotificationViewComponent : ViewComponent
  {
    private readonly UserManager<BeonUser> _userManager;
    private readonly ITopicSubscriptionRepository _tsRepository;
    private readonly ITopicRepository _topicRepository;
    public NewCommentsNotificationViewComponent(
        UserManager<BeonUser> userManager,
        ITopicRepository topicRepository,
        ITopicSubscriptionRepository tsRepository) {
      _userManager = userManager;
      _tsRepository = tsRepository;
      _topicRepository = topicRepository;
    }

    public async Task<IViewComponentResult> InvokeAsync() {
      BeonUser? u = await _userManager.GetUserAsync(UserClaimsPrincipal);
      if (u == null) {
        return View(new List<LinkViewModel>());
      }

      List<TopicSubscription> tss = await _tsRepository.TopicSubscriptions
        .Where(t => t.SubscriberId!.Equals(u.Id) && t.NewPosts.Equals(true))
        .Include(ts => ts.Topic)
        .ToListAsync();

      ICollection<LinkViewModel> links = new List<LinkViewModel>();
      foreach(var ts in tss) {
        if (ts.Topic != null) {
          links.Add(new LinkViewModel(ts.Topic.Title, await _topicRepository.GetTopicPathAsync(ts.Topic)));
        }
      }

      return View(links);
    }
  }
}