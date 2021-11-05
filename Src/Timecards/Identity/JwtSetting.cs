namespace Timecards.Identity
{
    public class JwtSetting
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string SecurityKey { get; set; }
    }
}