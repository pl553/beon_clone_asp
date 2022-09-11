namespace Beon.Models.ViewModels {
  public class HomeViewModel {
    public BoardViewModel Board;
    public HomeViewModel(BoardViewModel board) {
      Board = board;
    }
  }
}