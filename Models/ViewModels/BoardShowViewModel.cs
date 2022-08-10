namespace Beon.Models.ViewModels 
{
  public class BoardShowViewModel {
    public Board Board { get; set; }
    public TopicCreateViewModel newTopicForm { get; set; }

    public BoardShowViewModel(Board board) {
      Board = board;
      newTopicForm = new TopicCreateViewModel { boardId = board.BoardId };
    }
  }
}