namespace Timecards.Identity
{
    public class IdentitySetting
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public int ExpiredInDay { get; set; }
    }
}