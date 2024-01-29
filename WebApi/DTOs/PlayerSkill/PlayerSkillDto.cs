using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs.PlayerSkill
{
    public class PlayerSkillDto
    {
        public int Id { get; set; }
        public string Skill { get; set; }
        public int Value { get; set; }
        public int PlayerId { get; set; }
       
    }
}
