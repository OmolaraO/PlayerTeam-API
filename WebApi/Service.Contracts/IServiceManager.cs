namespace WebApi.Service.Contracts
{
    public interface IServiceManager
    {
        IPlayerService PlayerService { get; }
        IPlayerSkillService PlayerSkillService { get; }
    }
}
