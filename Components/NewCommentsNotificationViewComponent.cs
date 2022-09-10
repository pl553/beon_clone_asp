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
    private readonly TopicSubscriptionLogic _topicSubscriptionLogic;
    private readonly TopicLogic _topicLogic;
    public NewCommentsNotificationViewComponent(
        UserManager<BeonUser> userManager,
        TopicLogic topicLogic,
        TopicSubscriptionLogic topicSubscriptionLogic) {
      _userManager = userManager;
      _topicSubscriptionLogic = topicSubscriptionLogic;
      _topicLogic = topicLogic;
    }

    public async Task<IViewComponentResult> InvokeAsync() {
      BeonUser? u = await _userManager.GetUserAsync(UserClaimsPrincipal);
      if (u == null) {
        return View(new List<LinkViewModel>());
      }

      var tss = await _topicSubscriptionLogic.GetWithNewPosts(u.Id);

      ICollection<LinkViewModel> links = new List<LinkViewModel>();
      foreach(var ts in tss) {
        if (ts.Topic != null) {
          links.Add(new LinkViewModel(ts.Topic.Title, await _topicLogic.GetTopicPathAsync(ts.Topic)));
        }
      }

      return View(links);
    }
  }
}