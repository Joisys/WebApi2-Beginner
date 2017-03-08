using System.Data.Entity;
using WebApi2_Beginner.Entities;

namespace WebApi2_Beginner
{
    public class PropertyDbContext : DbContext
    {
        public PropertyDbContext() : base("PropertyEntities")
        {
            Database.SetInitializer(new PropertyDbInitializer());
        }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Property> Properties { get; set; }
    }
}