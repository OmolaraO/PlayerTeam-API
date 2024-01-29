using Microsoft.EntityFrameworkCore;
using WebApi.Contracts;
using WebApi.Entities;
using WebApi.Helpers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApi.Repository
{
    public class PlayerRepository : RepositoryBase<Player>, IPlayerRepository
    {
        public PlayerRepository(DataContext dataContext) : base(dataContext)
        {

        }

        public void CreatePlayer(Player player) => Create(player);

        public void DeletePlayer(Player player) => Delete(player);

        public async Task<IEnumerable<Player>> GetAllPlayersAsync(bool trackChanges) =>
            await FindAll(trackChanges).ToListAsync();


        public async Task<Player> GetPlayerByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(x => x.Id.Equals(id), trackChanges).FirstOrDefaultAsync();

        public Task<Player> GetPlayerByNameAsync(string name, bool trackChanges) =>
            FindByCondition(x => x.Name.Trim().ToLower().Equals(name.Trim().ToLower()), trackChanges).FirstOrDefaultAsync();

        public async Task<IEnumerable<Player>> GetPlayersBySkillAndPositionAsync(string position, string skill, bool trackChanges) =>
                await FindByCondition( x => x.Position.Equals(position) && x.PlayerSkills.Equals(skill), trackChanges).ToListAsync();

        public void UpdatePlayer(Player player) => Update(player);
    }
    
}
