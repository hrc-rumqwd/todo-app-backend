using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace TodoApp.Infrastructure.Options
{
    public class JwtConfigurationOptions : IConfigureOptions<JwtConfiguration>
    {
        public const string SectionKey = "Jwt";
        private readonly IConfiguration _configuration;

        public JwtConfigurationOptions(IConfiguration configuration)
        {
             _configuration = configuration;
        }

        public void Configure(JwtConfiguration options)
        {
            _configuration.GetSection(SectionKey).Bind(options);
        }
    }

    public class JwtConfiguration
    {
        public string SecretKey { get; set; }
        public long ExpiryMinutes { get; set; }
        public long RefreshTokenExpiryMinutes { get; set; }
        public string Audience { get; set; }
        public bool ValidateAudience { get; set; }
        public string Issuer { get; set; }
        public bool ValidateIssuer { get; set; }
    }
}
