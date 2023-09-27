namespace MyMooviesApi.Settings
{
    public class MongoConfig
    {
        public string Host { get; init; } = string.Empty;
        public string Port { get; init; } = string.Empty;
        public string Database { get; init; } = string.Empty;
        public string ConnectionString => $"mongodb://{Host}:{Port}";
    }
}
