using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Team2Application.Models;

namespace Team2Application.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Team2Application.Models.Intern> Intern { get; set; }
        public DbSet<Team2Application.Models.Skill> Skill { get; set; }
        public DbSet<Team2Application.Models.LibraryResource> LibraryResource { get; set; }
    }
}
