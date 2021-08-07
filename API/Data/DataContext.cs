using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        //DbSet takes the type of the class that we want to create a Database set for i.e., AppUser
        public DbSet<AppUser> Users { get; set; }
    }
}