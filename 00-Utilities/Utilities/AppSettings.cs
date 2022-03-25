namespace Utilities
{
    public record AppSettings
    {

        public DapperSetting Dapper { get; set; }
        public JwtInfo Jwt { get; set; }
    }
    public record DapperSetting
    {
        public string DapperCnn { get; set; }
    }
    public record JwtInfo
    {
        public string Key { get; set; }
        public string EncryptKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int DurationInMinutes { get; set; }
    }
}
