using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test3.Models;
using Microsoft.EntityFrameworkCore;

namespace test3.Models
{
    public class DbEntity : DbContext
    {
        public DbEntity(DbContextOptions<DbEntity> options) : base(options)
        { }
        public DbSet<Customers> Customers { get; set; }
    }
}
