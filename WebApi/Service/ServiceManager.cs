using Microsoft.Extensions.Logging;
using WebApi.Contracts;
using WebApi.Repository;
using WebApi.Service.Contracts;

namespace WebApi.Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IPlayerService> _playerService;
        private readonly Lazy<IPlayerSkillService> _playerSkillService;

        public ServiceManager(IRepositoryManager repositoryManager)
        {

            _playerService = new Lazy<IPlayerService>(() => new PlayerService(repositoryManager));
            _playerSkillService = new Lazy<IPlayerSkillService>(() => new PlayerSkillService(repositoryManager));

        }

        public IPlayerService PlayerService => _playerService.Value;

        public IPlayerSkillService PlayerSkillService => _playerSkillService.Value;

    }
}
