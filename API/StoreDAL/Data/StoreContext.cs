using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StoreDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDAL.Data
{
    public class StoreContext : IdentityDbContext<User, Role, int>
    {
        public StoreContext()
        {
        }
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Sight> Sights { get; set; }
        public DbSet<SightPhoto> SightPhotos { get; set; }
    }
}
