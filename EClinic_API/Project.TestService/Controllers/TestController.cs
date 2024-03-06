using Amazon.S3;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Enum;
using Project.Common.Security;
using Project.Core.AWS;
using System.Security.Policy;

namespace Project.TestService.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IAmazonS3Bucket bucket;

        public TestController(IAmazonS3Bucket bucket)
        {
            this.bucket = bucket;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var x = await bucket.UploadFileAsync(file, FileType.Image);
            return Ok(x);
        }
        [HttpGet]
        public async Task<IActionResult> GetUrl(string key)
        {
            var x = await bucket.GetFileAsync(key);
            return Ok(x);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteFile(string key)
        {
            var x = await bucket.DeleteFileAsync(key);
            return Ok(x);
        }
        [HttpGet]
        public IActionResult PaswordGenerate(string password)
        {
            var x = Cryptography.EncryptPassword(password);
            var z = BitConverter.ToString(x.Salt).Replace("-", string.Empty);
            var salt = "0x" + z;
            var result = new { hash = x.Hash, salt = salt };
            return Ok(result);
        }
    }
}
