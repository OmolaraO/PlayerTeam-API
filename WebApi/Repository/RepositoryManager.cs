using WebApi.Contracts;
using WebApi.Helpers;

namespace WebApi.Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly DataContext _dataContext;
        private readonly Lazy<IPlayerRepository> _playerRepository;
        private readonly Lazy<IPlayerSkillRepository> _playerSkillRepository;

        public RepositoryManager(DataContext dataContext)
        {
            _dataContext = dataContext;
            _playerRepository = new Lazy<IPlayerRepository>(() => new PlayerRepository(dataContext));
            _playerSkillRepository = new Lazy<IPlayerSkillRepository>(() => new PlayerSkillRepository(dataContext));

        }

        public IPlayerRepository Player => _playerRepository.Value;

        public IPlayerSkillRepository PlayerSkill => _playerSkillRepository.Value;

        public async Task SaveAsync() => await _dataContext.SaveChangesAsync();       
       
    }
}
