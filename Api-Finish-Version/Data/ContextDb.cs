using Api_Finish_Version.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace Api_Finish_Version.Data
{
    public class ContextDb : DbContext
    {
        public ContextDb(DbContextOptions<ContextDb> options)
            : base(options)
        {
        }

        //LOGIN REGISTER
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
