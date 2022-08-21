namespace Beon.Models {
  public interface IBoardRepository {
    IQueryable<Board> Boards { get; }

    void SaveBoard(Board board);
    void UpdateBoard(Board board);
  }
}