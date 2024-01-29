using WebApi.Entities;

namespace WebApi.Contracts
{
    public interface IPlayerSkillRepository
    {
        void CreatePlayerSkill(PlayerSkill playerSkill);

        void BulkCreatePlayerSkill(List<PlayerSkill> playerSkills);
        void UpdatePlayerSkill(PlayerSkill playerSkill);
        void DeletePlayerSkill(PlayerSkill playerSkill);

        Task<PlayerSkill> GetPlayerSkillAsync(string skill, bool trackChanges);
        Task<IEnumerable<PlayerSkill>> GetPlayerSkills(int playerId, bool trackChanges);

    }
}
