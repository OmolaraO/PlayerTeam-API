using WebApi.Contracts;
using WebApi.Service.Contracts;

namespace WebApi.Service
{
    public class PlayerSkillService : IPlayerSkillService
    {
        private readonly IRepositoryManager _repository;

        public PlayerSkillService(IRepositoryManager repository)
        {
            _repository = repository;
        }
    }
}
