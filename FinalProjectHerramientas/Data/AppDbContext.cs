using FinalProjectHerramientas.Model;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectHerramientas.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Productos> Productos { get; set; }
    }
}
