using WebApi.DTOs.PlayerSkill;
using WebApi.Entities;

namespace WebApi.DTOs.Player
{
    public class PlayerForCreationDto
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public IEnumerable<PlayerSkillForCreationDto> PlayerSkills { get; set; }
    }
}
