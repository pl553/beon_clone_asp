namespace Beon.Models.ViewModels 
{
  public class BoardShowViewModel {
    public Board Board;
    public TopicFormViewModel newTopicForm;

    public BoardShowViewModel(Board board) {
      Board = board;
      newTopicForm = new TopicFormViewModel { boardId = board.BoardId };
    }
  }
}