using Dominio;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura
{
    public class ConversorLogContexto : DbContext
    {
        public ConversorLogContexto(DbContextOptions<ConversorLogContexto> options) : base(options) { }

        public DbSet<Log> Logs { get; set; }
        
    }
}
