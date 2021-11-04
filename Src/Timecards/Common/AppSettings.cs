using Microsoft.Extensions.Configuration;

namespace Timecards.Common
{
    public class AppSettings
    {
        private static IConfiguration _configuration;

        public AppSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public static IConfiguration Current => _configuration; 
    }
}