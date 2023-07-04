namespace SimpleStore.Entities
{
    public class AppConfig
    {
        public Storageconfiguration StorageConfiguration { get; set; } = default!;
        public Jwt Jwt { get; set; } = default!;
        public SmtpConfiguration SmtpConfiguration { get; set; } = default!;
        public string WebAppUrl { get; set; } = default!;
    }

    public class Storageconfiguration
    {
        public string PublicUrl { get; set; } = default!;
        public string Path { get; set; } = default!;
        public string ContainerName { get; set; } = default!;
    }

    public class Jwt
    {
        public string SecretKey { get; set; } = default!;
        public string Audience { get; set; } = default!;
        public string Issuer { get; set; } = default!;
    }

    public class SmtpConfiguration
    {
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Server { get; set; } = default!;
        public int Port { get; set; } = default!;
        public string From { get; set; } = default!;
        public bool EnableSsl { get; set; } = default!;

    }
}