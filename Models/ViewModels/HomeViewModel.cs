namespace Beon.Models.ViewModels {
  public class HomeViewModel {
    public BoardShowViewModel Board;
    public HomeViewModel(BoardShowViewModel board) {
      Board = board;
    }
  }
}