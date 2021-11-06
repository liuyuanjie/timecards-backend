namespace Timecards.Application.Exceptions
{
    public class InvalidIdentityException : ApiCustomException
    {
        public InvalidIdentityException() : base("InvalidLogin", "Failed to login.")
        {
        }
    }
}