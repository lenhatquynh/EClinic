namespace Project.Core.Model
{
    public class JWTOptions
    {
        public string ValidAudience { get; init; }
        public string ValidIssuer { get; init; }
        public string SecretKey { get; init; }
    }
}
