namespace WebApi.Exceptions
{
    public class PlayerPositionBadRequest : BadRequestException
    {
        public PlayerPositionBadRequest(string message) : base(message)
        {
        }
    }
}
