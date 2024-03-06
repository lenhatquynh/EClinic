namespace Project.Core.Model
{
    internal class TokenOptions
    {
        public int ExpireAccessToken { get; set; } = 0;
        public int ExpireRefreshToken { get; set; } = 0;
    }
}
