namespace Beon.Models.ViewModels {
  public class DiaryViewModel {
    public BoardShowViewModel Board { get; set; }
    public DiaryViewModel(BoardShowViewModel board) {
      Board = board;
    }
  }
}