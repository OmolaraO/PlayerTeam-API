namespace WebApi.Exceptions
{
    public class DuplicatePlayerException : DuplicateRequestException
    {
        public DuplicatePlayerException(string message) : base(message)
        {
        }
    }
}
