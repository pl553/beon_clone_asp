using Microsoft.AspNetCore.Mvc;
using Beon.Models;
using Beon.Services;
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
    private readonly TopicSubscriptionService _topicSubscriptionService;
    public NewCommentsNotificationViewComponent(
      UserManager<BeonUser> userManager,
      TopicSubscriptionService topicSubscriptionService)
    {
      _userManager = userManager;
      _topicSubscriptionService = topicSubscriptionService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
      BeonUser? u = await _userManager.GetUserAsync(UserClaimsPrincipal);
      if (u == null) {
        return View(new List<LinkViewModel>());
      }

      var tss = await _topicSubscriptionService.GetWithNewPostsAsync(u.Id);

      var links = await Task.WhenAll(
        tss.Select(
          async ts => new LinkViewModel(
            (await ts.GetTopicAsync()).Title,
            await (await ts.GetTopicAsync()).GetPathAsync())));
      
      return View(links);
    }
  }
}