using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team2Application.Models
{
    public class Skill
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public string Description { get; set; }

        public string SkillMatrixUrl { get; set; }

        public List<Skill_LibraryResource> Skill_LibraryResources { get; set; }
    }
}
