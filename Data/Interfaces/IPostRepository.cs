namespace Beon.Models {
  public interface IPostRepository {
    IQueryable<Post> Posts { get; }

    void SavePost(Post Post);
  }
}