using Microsoft.EntityFrameworkCore;
using WeaponsAPI.Weapons.Model;

namespace WeaponsAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public virtual DbSet<Weapon> Weapons { get; set; }
    }
}
