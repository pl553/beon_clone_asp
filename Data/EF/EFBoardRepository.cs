using Microsoft.EntityFrameworkCore;

namespace Beon.Models {
  public class EFBoardRepository : IBoardRepository {
    private BeonDbContext context;
    public EFBoardRepository(BeonDbContext ctx) {
      context = ctx;
    }

    public void SaveBoard(Board board) {
      context.Boards.Add(board);
      context.SaveChanges();
    }
    public IQueryable<Board> Boards => context.Boards;
  }
}