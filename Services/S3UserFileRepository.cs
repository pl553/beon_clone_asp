using Amazon.S3;
using Amazon.S3.Model;

namespace Beon.Services {
  public class S3UserFileRepository : IUserFileRepository {
    private readonly string _userFilesPath;
    private readonly AmazonS3Client _s3Client;
    private readonly string _bucket;
    private readonly string _endpoint;
    public S3UserFileRepository(string userFilesPath, string endpoint, string accessKeyId, string secretKey, string bucket) {
      if (userFilesPath != "" && (userFilesPath[0] == '/' || userFilesPath[0] == '\\')) {
        throw new Exception("user files path must be a relative path");
      }
      _userFilesPath = userFilesPath;
      _bucket = bucket;
      _endpoint = endpoint;
      _s3Client = new AmazonS3Client(accessKeyId, secretKey, new AmazonS3Config { ServiceURL = endpoint });
    }
    public async Task SaveFileAsync(string userName, string fileName, IFormFile file) {
      using (var stream = new MemoryStream()) {
        await file.CopyToAsync(stream);
        PutObjectRequest req = new PutObjectRequest
        {
          BucketName = _bucket,
          Key = GetFilePath(userName, fileName),
          InputStream = stream,
        };
        req.Headers.CacheControl = "max-age=31536000, immutable";
        await _s3Client.PutObjectAsync(req);
      }
    }
    public async Task DeleteFileAsync(string userName, string fileName) {
      var req = new DeleteObjectRequest
      {
        BucketName = _bucket,
        Key = GetFilePath(userName, fileName)
      };
      await _s3Client.DeleteObjectAsync(req);
    }
    public Task<string> GetFileUrlAsync(string userName, string fileName) {
      return Task.FromResult<string>(_endpoint + "/" + Path.Combine(_bucket, _userFilesPath, userName, fileName));
    }

    private string GetFilePath(string userName, string fileName) {
      return Path.Combine(_userFilesPath, userName, fileName);
    }
  }
}