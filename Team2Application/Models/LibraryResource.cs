using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team2Application.Models
{
    public class LibraryResource
    {

        public LibraryResource()
        {
               
        }

        public LibraryResource(string name, string recommandation, string url)
        {
            Name = name;
            Recommandation = recommandation;
            Url = url;
        }

        public string Name { get; set; }

        public int Id { get; set; }

        public string Recommandation { get; set; }

        public string Url { get; set; }

        /*        public void Play()
                {
                    *//*Starts playing video*//*
                }

                public void RetriveInformation()
                {
                    *//*Retrievs information*//*
                }*/
        public List<Skill_LibraryResource> Skill_LibraryResources { get; set; }
    }
}
