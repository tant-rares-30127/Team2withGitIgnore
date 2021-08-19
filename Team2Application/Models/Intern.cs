using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team2Application.Models
{
    public class Intern
    {
      

        public string Name { get; set; }

        public int Id { get; set; }

        public DateTime Birthdate { get; set; }

        public string EmailAddress { get; set; }

        public List<Intern_Skill> Intern_Skill { get; set; }


        public int GetAge()
        {
            DateTime dateOfBirth = this.Birthdate;
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear) age = age - 1;
            return age;
        }
    }
}
