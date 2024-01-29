namespace WebApi.Exceptions
{
    public class PlayerSkillBadRequestException : BadRequestException
    {
        public PlayerSkillBadRequestException(string message) : base(message)
        {
        }
    }
}
