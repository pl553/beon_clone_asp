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

    public void UpdateBoard(Board board) {
      context.Entry(board).State = EntityState.Modified;
      context.SaveChanges();
    }
    public IQueryable<Board> Boards => context.Boards;
  }
}