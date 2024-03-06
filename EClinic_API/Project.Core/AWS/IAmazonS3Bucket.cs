using Microsoft.AspNetCore.Http;
using Project.Common.Enum;

namespace Project.Core.AWS
{
    public interface IAmazonS3Bucket
    {
        Task<List<string>> GetAllKeyAsync();
        Task<string> GetFileAsync(string Key);
        Task<List<string>> GetManyFileAsync(List<string> Keys);
        Task<string> UploadFileAsync(IFormFile File, FileType FileType);
        Task<List<string>> UploadManyFileAsync(List<IFormFile> Files, FileType FileType);
        Task<bool> DeleteFileAsync(string Key);
        Task<bool> DeleteManyFileAsync(List<string> Keys);
    }
}
