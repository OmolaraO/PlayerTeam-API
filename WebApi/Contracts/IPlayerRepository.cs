using WebApi.Entities;

namespace WebApi.Contracts
{
    public interface IPlayerRepository
    {
        Task<IEnumerable<Player>> GetAllPlayersAsync(bool trackChanges);
        Task<Player> GetPlayerByIdAsync(int id, bool trackChanges);

        Task<Player> GetPlayerByNameAsync(string name, bool trackChanges);

        Task<IEnumerable<Player>> GetPlayersBySkillAndPositionAsync(string position, string skill, bool trackChanges);

        void CreatePlayer(Player player);
        void UpdatePlayer(Player player);

        void DeletePlayer(Player player);
    }
}
