using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team2Application.Models
{
    public class LibraryResource
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public string Recommandation { get; set; }

        public string Url { get; set; }

        public void Play()
        {
            /*Starts playing video*/
        }

        public void RetriveInformation()
        {
            /*Retrievs information*/
        }
    }
}
