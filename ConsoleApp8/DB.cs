using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8
{
    internal class DB : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DB(DbContextOptions<DB> options)
            : base(options)
        {
            Database.EnsureCreated();
        }


    }
}
