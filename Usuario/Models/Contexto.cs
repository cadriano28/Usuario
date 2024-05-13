using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Usuario.Models;

namespace Usuario.Models
{
    public class Contexto: DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) 
        {
            Users = Set<Users>();
        }

        public DbSet<Users> Users { get; set; }
    }
}
