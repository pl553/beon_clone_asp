namespace Beon.Models.ViewModels {
  public class PagingInfo {
    public string BaseUrl { get; set; }
    public int NumPages { get; set; }
    public int TotalShown { get; set; }
    public int CurrentPage { get; set; }
    public PagingInfo(string baseUrl, int currentPage, int numPages, int totalShown = 10) {
      if (baseUrl.Length == 0 || baseUrl[0] != '/') {
        throw new Exception("baseUrl must be absolute");
      }
      if (baseUrl[baseUrl.Length-1] == '/') {
        baseUrl = baseUrl.Remove(baseUrl.Length-1, 1);
      }
      BaseUrl = baseUrl;
      NumPages = numPages;
      TotalShown = totalShown;
      CurrentPage = currentPage;
    }
  }
  public class HrBarViewModel {
    public ICollection<LinkViewModel>? Crumbs { get; set; }
    public PagingInfo? PagingInfo { get; set; }
    public DateTime? TimeStamp { get; set; }
    public HrBarViewModel(ICollection<LinkViewModel>? crumbs = null, DateTime? timeStamp = null, PagingInfo? pagingInfo = null) {
      Crumbs = crumbs;
      TimeStamp = timeStamp;
      PagingInfo = pagingInfo;
    }
  }
}