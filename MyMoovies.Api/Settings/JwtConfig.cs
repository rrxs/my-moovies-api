namespace MyMooviesApi.Settings
{
    public class JwtConfig
    {
        public string Issuer { get; init; }
        public string Audience { get; init; }
        public string SecretKey { get; init; }
    }
}
