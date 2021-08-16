
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

        public DbSet<Intern> Intern { get; set; }
        public DbSet<Skill> Skill { get; set; }
        public DbSet<LibraryResource> LibraryResource { get; set; }

    }
}
