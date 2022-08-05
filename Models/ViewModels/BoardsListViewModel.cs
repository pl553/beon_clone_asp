namespace Beon.Models.ViewModels
{
  public class BoardsListViewModel
  {
    public IEnumerable<Board> Boards { get; set; } = Enumerable.Empty<Board>();
  }
}