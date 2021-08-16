using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team2Application.Models
{
    public class Skill_LibraryResource
    {
            public int Id { get; set; }

            public int SkillId { get; set; }
            public Skill Skill { get; set; }

            public int LibraryResourceId { get; set; }
            public LibraryResource LibraryResource { get; set; }
    }
}
