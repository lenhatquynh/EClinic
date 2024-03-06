using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Project.Common.Constants;
using Project.Common.Enum;
using Project.Core.Logger;

namespace Project.Core.AWS
{
    public class AmazonS3Bucket : IAmazonS3Bucket
    {
        private readonly IAmazonS3 s3Client;
        private readonly ILogger<AmazonS3Bucket> logger;

        public AmazonS3Bucket(IAmazonS3 s3Client, ILogger<AmazonS3Bucket> logger)
        {
            this.s3Client = s3Client;
            this.logger = logger;
        }

        public async Task<bool> IsBucketExistsAsync()
        {
            try
            {
                var listBucketsResponse = await s3Client.ListBucketsAsync();
                var bucketName = Bucket.BucketName;
                if (listBucketsResponse.Buckets.Any(b => b.BucketName == bucketName))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return false;
            }
        }
        private string GetPrefix(FileType FileType)
        {
            string prefix = "";
            switch (FileType)
            {
                case FileType.Image:
                    prefix = "Image";
                    break;
                case FileType.Video:
                    prefix = "Video";
                    break;
                case FileType.Docs:
                    prefix = "Docs";
                    break;
                case FileType.OtherFile:
                    prefix = "OtherFile";
                    break;
            }
            return prefix;
        }
        public async Task<string> GetFileAsync(string Key)
        {
            try
            {
                var bucket = Bucket.BucketName;
                var bucketExists = await IsBucketExistsAsync();
                if (!bucketExists)
                {
                    throw new Exception($"Bucket {bucket} does not exist.");
                }

                var expiryUrlRequest = new GetPreSignedUrlRequest()
                {
                    BucketName = bucket,
                    Key = Key,
                    Expires = DateTime.UtcNow.AddYears(1)
                };

                string url = s3Client.GetPreSignedURL(expiryUrlRequest);
                return url;
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return null;
            }
        }

        public async Task<List<string>> GetManyFileAsync(List<string> Keys)
        {
            try
            {
                var bucket = Bucket.BucketName;
                var bucketExists = await IsBucketExistsAsync();
                if (!bucketExists)
                {
                    throw new Exception($"Bucket {bucket} does not exist.");
                }
                List<string> urls = new List<string>();
                foreach (var Key in Keys)
                {

                    var expiryUrlRequest = new GetPreSignedUrlRequest()
                    {
                        BucketName = bucket,
                        Key = Key,
                        Expires = DateTime.UtcNow.AddYears(1)
                    };

                    string url = s3Client.GetPreSignedURL(expiryUrlRequest);
                    urls.Add(url);
                }
                return urls;
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return null;
            }

        }

        public async Task<string> UploadFileAsync(IFormFile File, FileType FileType)
        {
            try
            {
                var prefix = GetPrefix(FileType);
                var bucket = Bucket.BucketName;
                var bucketExists = await IsBucketExistsAsync();
                if (!bucketExists)
                {
                    throw new Exception($"Bucket {bucket} does not exist.");
                }
                var fileName = $"{DateTime.Now}_{File.FileName}";
                fileName = fileName.Replace("/", "");
                fileName = fileName.Replace(" ", "_");
                var key = $"{prefix}/{fileName}";
                var request = new PutObjectRequest()
                {
                    BucketName = bucket,
                    Key = key,
                    InputStream = File.OpenReadStream()
                };
                var response = await s3Client.PutObjectAsync(request);
                if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception("File uploaded failed");
                }
                var url = await GetFileAsync(key);
                return url;
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return null;
            }
        }

        public async Task<List<string>> UploadManyFileAsync(List<IFormFile> Files, FileType FileType)
        {
            try
            {
                var prefix = GetPrefix(FileType);
                var bucket = Bucket.BucketName;
                var bucketExists = await IsBucketExistsAsync();
                List<string> Keys = new List<string>();
                if (!bucketExists)
                {
                    throw new Exception($"Bucket {bucket} does not exist.");
                }
                foreach (var File in Files)
                {
                    var fileName = $"{DateTime.Now}_{File.FileName}";
                    fileName = fileName.Replace("/", "");
                    fileName = fileName.Replace(" ", "_");
                    var key = $"{prefix}/{fileName}";
                    var request = new PutObjectRequest()
                    {
                        BucketName = bucket,
                        Key = key,
                        InputStream = File.OpenReadStream()
                    };
                    var response = await s3Client.PutObjectAsync(request);
                    if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
                    {
                        throw new Exception("File uploaded failed");
                    }
                    Keys.Add(key);
                }
                var urls = await GetManyFileAsync(Keys);
                return urls;
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> DeleteFileAsync(string Key)
        {
            try
            {
                var bucket = Bucket.BucketName;
                var bucketExists = await IsBucketExistsAsync();
                if (!bucketExists)
                {
                    throw new Exception($"Bucket {bucket} does not exist.");
                }

                var deleteRequest = new DeleteObjectRequest
                {
                    BucketName = Bucket.BucketName,
                    Key = Key
                };

                var result = await s3Client.DeleteObjectAsync(deleteRequest);
                if (result.HttpStatusCode != System.Net.HttpStatusCode.NoContent)
                {
                    throw new Exception("Delete file failed");
                }
                return true;
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return false;
            }
        }
        public async Task<bool> DeleteManyFileAsync(List<string> Keys)
        {
            try
            {
                var bucket = Bucket.BucketName;
                var bucketExists = await IsBucketExistsAsync();
                if (!bucketExists)
                {
                    throw new Exception($"Bucket {bucket} does not exist.");
                }

                foreach (var Key in Keys)
                {
                    var deleteRequest = new DeleteObjectRequest
                    {
                        BucketName = Bucket.BucketName,
                        Key = Key
                    };

                    var result = await s3Client.DeleteObjectAsync(deleteRequest);
                    if (result.HttpStatusCode != System.Net.HttpStatusCode.NoContent)
                    {
                        throw new Exception("Delete file failed");
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return false;
            }
        }

        public async Task<List<string>> GetAllKeyAsync()
        {
            try
            {
                var objectKeys = new List<string>();
                var listRequest = new ListObjectsV2Request
                {
                    BucketName = Bucket.BucketName,
                };

                ListObjectsV2Response listResponse;
                do
                {
                    listResponse = await s3Client.ListObjectsV2Async(listRequest);
                    objectKeys.AddRange(listResponse.S3Objects.Select(x => x.Key));
                    listRequest.ContinuationToken = listResponse.NextContinuationToken;
                } while (listResponse.IsTruncated);

                return objectKeys;
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return null;
            }
        }
    }
}
