namespace Beon.Services {
  public class DiskUserFileRepository : IUserFileRepository {
    private readonly string _userFilesPath;
    public DiskUserFileRepository(string userFilesPath) {
      if (userFilesPath != "" && (userFilesPath[0] == '/' || userFilesPath[0] == '\\')) {
        throw new Exception("user files path must be a relative path");
      }
      _userFilesPath = userFilesPath;
    }
    public async Task SaveFileAsync(string userName, string fileName, IFormFile file) {
      var path = GetDiskPath(userName, fileName);
      var dir = new FileInfo(path).DirectoryName;
      if (dir != null) {
        System.IO.Directory.CreateDirectory(dir);
      }
      using (var stream = System.IO.File.Create(path)) {
          await file.CopyToAsync(stream);
      }
    }
    public Task DeleteFileAsync(string userName, string fileName) {
      System.IO.File.Delete(GetDiskPath(userName, fileName));
      return Task.CompletedTask;
    }
    public Task<string> GetFileUrlAsync(string userName, string fileName) {
      return Task.FromResult<string>(GetSitePath(userName, fileName));
    }

    private string GetSitePath(string userName, string fileName) {
      return Path.Combine("/", _userFilesPath, userName, fileName);
    }

    private string GetDiskPath(string userName, string fileName) {
      return Path.Combine("wwwroot/", _userFilesPath, userName, fileName);
    }
  }
}