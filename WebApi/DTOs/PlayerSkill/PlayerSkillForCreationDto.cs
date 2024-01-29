using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs.PlayerSkill
{
    public class PlayerSkillForCreationDto
    {
        public string Skill { get; set; }
        public int Value { get; set; }
    }
}
