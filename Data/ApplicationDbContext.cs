using contactBook.Model;
using Microsoft.EntityFrameworkCore;

namespace contactBook.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        //the Dbset property will help create a
        // table called user using EF core if it dosent exist
        public virtual DbSet<User> Users { get; set; }
    }
}
