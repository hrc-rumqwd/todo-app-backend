namespace TodoApp.Shared.Domains.Auth
{
    public class TokenResult
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiry { get; set; }
    }
}
