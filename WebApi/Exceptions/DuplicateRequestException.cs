namespace WebApi.Exceptions
{
    public class DuplicateRequestException : Exception
    {
        protected DuplicateRequestException(string message) : base(message) { }
    }
}
