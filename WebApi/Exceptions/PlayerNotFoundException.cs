namespace WebApi.Exceptions
{
    public class PlayerNotFoundException : NotFoundException
    {
        public PlayerNotFoundException(string message) : base(message)
        {
        }
    }
}
