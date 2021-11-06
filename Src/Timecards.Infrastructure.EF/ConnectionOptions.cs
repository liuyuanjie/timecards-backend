namespace Timecards.Infrastructure.EF
{
    public class ConnectionOptions
    {
        public const string Position = "ConnectionStrings";

        public string DefaultConnection { get; set; }
    }
}