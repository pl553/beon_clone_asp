namespace Beon.Models.ViewModels {
  public class LinkViewModel {
    public string Text;
    public string Url;

    public LinkViewModel(string text, string url) {
      Text = text;
      Url = url;
    }
  }
}