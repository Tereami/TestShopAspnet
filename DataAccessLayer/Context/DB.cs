using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DomainModel.Enitities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DomainModel.Identity;

namespace DataAccessLayer.Context
{
    public class DB : IdentityDbContext<User, Role, string>
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Person> Persons { get; set; }

        public DB(DbContextOptions<DB> options) : base(options)
        {

        }
    }
}
