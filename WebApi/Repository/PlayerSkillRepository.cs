using Microsoft.EntityFrameworkCore;
using WebApi.Contracts;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Repository
{
    public class PlayerSkillRepository : RepositoryBase<PlayerSkill>, IPlayerSkillRepository
    {
        public PlayerSkillRepository(DataContext dataContext) : base(dataContext)   
        {

        }

        public void BulkCreatePlayerSkill(List<PlayerSkill> playerSkills) => BulkCreate(playerSkills);

        public void CreatePlayerSkill(PlayerSkill playerSkill) => Create(playerSkill);
       


        public void DeletePlayerSkill(PlayerSkill playerSkill) => Delete(playerSkill);

        public async Task<PlayerSkill> GetPlayerSkillAsync(string skill, bool trackChanges) =>
            await FindByCondition(x => x.Skill.Trim().ToLower().Equals(skill.Trim().ToLower()), trackChanges).FirstOrDefaultAsync();
       

        public async Task<IEnumerable<PlayerSkill>> GetPlayerSkills(int playerId, bool trackChanges) =>
            await FindByCondition(x => x.PlayerId.Equals(playerId), trackChanges).ToListAsync();
   

        public void UpdatePlayerSkill(PlayerSkill playerSkill) => Update(playerSkill);  
       
    }
}
