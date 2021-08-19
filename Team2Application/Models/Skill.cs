using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team2Application.Models
{
    public class Skill
    {
        public Skill()
        {

        }

        public Skill(string name, string skillMatrixUrl, string description)
        {
            Name = name;
            SkillMatrixUrl = skillMatrixUrl;
            Description = description;
        }

        public string Name { get; set; }

        public int Id { get; set; }

        public string Description { get; set; }

        public string SkillMatrixUrl { get; set; }

        public List<Skill_LibraryResource> Skill_LibraryResources { get; set; }
    }
}
