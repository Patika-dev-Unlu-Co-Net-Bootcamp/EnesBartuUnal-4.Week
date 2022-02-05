using ManagerApi4.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerApi4.Context
{
    public class ManagerDbContext : IdentityDbContext<User>
    {
        public ManagerDbContext(DbContextOptions<ManagerDbContext> options) : base(options)
        {

        }

        public DbSet<Player> Players { get; set; }

        public DbSet<Team> Teams { get; set; }
              
    }

    
}
