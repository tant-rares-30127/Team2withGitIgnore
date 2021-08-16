namespace Team2Application.Models
{
    public class Intern_Skill
    {
        public int Id { get; set; }

        public int InternId { get; set; }
        public Intern Intern { get; set; }

        public int SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}
