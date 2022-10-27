using Microsoft.EntityFrameworkCore;

using Beon.Models;
using Beon.Models.ViewModels;

namespace Beon.Services
{
  public class TopicService
  {
    private readonly IRepository<Topic> _topicRepository;

    public TopicService(
      IRepository<Topic> topicRepository)
    {
      _topicRepository = topicRepository;
    }

    public async Task<bool> TopicExistsAsync(int topicPostId)
      => await _topicRepository.Entities
        .Where(t => t.PostId == topicPostId)
        .CountAsync() > 0;
  }
}