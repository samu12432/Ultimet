using Api_Finish_Version.Models.Auth;
using Api_Finish_Version.Models.Supply;
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

        //SUPPLY
        public DbSet<Supply> Supplies { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Glass> Glasses { get; set; }
        public DbSet<Accessory> Accessories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
