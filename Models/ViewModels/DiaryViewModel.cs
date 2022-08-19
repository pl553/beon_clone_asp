namespace Beon.Models.ViewModels {
  public class DiaryViewModel {
    public string UserName { get; set; }
    public BoardShowViewModel Board { get; set; }
    public DiaryViewModel(BoardShowViewModel board, string userName) {
      Board = board;
      UserName = userName;
    }
  }
}