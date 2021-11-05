using Timecards.Common;

namespace Timecards.Identity
{
    public static class JwtSetting
    {
        public static int ExpiredDateInDay => int.Parse(AppSettings.Current["Jwt:ExpiredDateInDay"]);
        public static string Issuer => AppSettings.Current["Jwt:Issuer"];
        public static string SecurityKey => AppSettings.Current["Jwt:SecurityKey"];
    }
}