using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jan24ft_bet_ca_kronosGR.Models;
using Microsoft.EntityFrameworkCore;

namespace jan24ft_bet_ca_kronosGR
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Developer> Developers { get; set; }
    }
}