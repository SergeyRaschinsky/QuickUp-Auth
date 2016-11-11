namespace AuthorizationExample.Server.Common.Models.Authentication
{
    public class AuthorizationSettings
    {
        public string SecretKey { get; set; }

        public string Audience { get; set; }

        public string Issuer { get; set; }

        public double ExpirationDays { get; set; }
    }
}
