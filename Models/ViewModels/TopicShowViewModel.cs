namespace Beon.Models.ViewModels 
{
  public class TopicShowViewModel {
    public Topic Topic { get; set; }
    public PostCreateViewModel NewPost { get; set; }

    public TopicShowViewModel(Topic _Topic) {
      Topic = _Topic;
      NewPost = new PostCreateViewModel { TopicId = _Topic.TopicId, BoardType = _Topic.Board!.Type, BoardOwnerName = _Topic.Board!.OwnerName };
    }
  }
}