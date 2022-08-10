namespace Beon.Models {
  public class EFPostRepository : IPostRepository {
    private BeonDbContext context;
    public EFPostRepository(BeonDbContext ctx) {
      context = ctx;
    }

    public void SavePost(Post Post) {
      context.Posts.Add(Post);
      context.SaveChanges();
    }
    public IQueryable<Post> Posts => context.Posts;
  }
}