namespace Beon.Models.ViewModels {
  public class DiaryViewModel {
    public string UserName { get; set; }
    public BoardViewModel Board { get; set; }
    public DiaryViewModel(BoardViewModel board, string userName) {
      Board = board;
      UserName = userName;
    }
  }
}