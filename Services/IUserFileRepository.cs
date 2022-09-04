namespace Beon.Services {
  public interface IUserFileRepository {
    public Task SaveFileAsync(string userName, string fileName, IFormFile file);
    public Task DeleteFileAsync(string userName, string fileName);
    public Task<string> GetFileUrlAsync(string userName, string fileName);
  }
}