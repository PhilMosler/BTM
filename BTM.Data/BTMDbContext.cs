using BTM.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTM.Data
{
    public class BTMDbContext:DbContext
    {
        public BTMDbContext(DbContextOptions<BTMDbContext> options) : base(options)
        {
                
        }
        public DbSet<Devices> Devices { get; set; }
        public DbSet<Telefon> Telephone { get; set; }
        public DbSet<Kunde> Customers { get; set; }
        public DbSet<Counters> Counters { get; set; }
    }
}
