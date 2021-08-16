using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team2Application.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string SkillMatrixUrl { get; set; }

        public List<Intern_Skill> Intern_Skill { get; set; }

        public List<Skill_LibraryResource> Skill_LibraryResource { get; set; }

        //public List<LibraryResource> GetResources()
        //{
          //  return LibraryResources;
        //}
    }
}
