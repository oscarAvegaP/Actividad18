using Actividad18.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Actividad18.Server.Contexto
{
    public class ContextoAutos : DbContext
    {
        public ContextoAutos(DbContextOptions<ContextoAutos> options) :base (options)
        {

        }
        public DbSet<Coches> Coches { get; set; }
        public DbSet<Cliente> Cliente { get; set; }

        public DbSet<Alquiler> Alquilers { get; set;}
    }
}
