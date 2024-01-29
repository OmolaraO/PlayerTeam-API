using WebApi.DTOs.PlayerSkill;

namespace WebApi.DTOs.Player
{
    public class PlayerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public IEnumerable<PlayerSkillDto> PlayerSkills { get; set; }
    }
}
