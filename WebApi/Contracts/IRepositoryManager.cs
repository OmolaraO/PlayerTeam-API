namespace WebApi.Contracts
{
    public interface IRepositoryManager
    {
        IPlayerRepository Player { get; }
        IPlayerSkillRepository PlayerSkill { get; }
        Task SaveAsync();   
    }
}
