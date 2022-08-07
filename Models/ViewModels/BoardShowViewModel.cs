namespace Beon.Models.ViewModels 
{
  public class BoardShowViewModel {
    public Board Board { get; set; }
    public TopicFormViewModel newTopicForm { get; set; }

    public BoardShowViewModel(Board board) {
      Board = board;
      newTopicForm = new TopicFormViewModel { boardId = board.BoardId };
    }
  }
}