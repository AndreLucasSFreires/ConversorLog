using Dominio;
using Infraestrutura.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Repositorios
{
    public class RepositorioLog : ILogRepositorio
    {
        private readonly ConversorLogContexto _contexto;

        public RepositorioLog(ConversorLogContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<Log> ObterPorIdAsync(int id)
        {
            return await _contexto.Logs.FindAsync(id);
        }

        public async Task<List<Log>> ObterTodos()
        {
            return await _contexto.Logs.ToListAsync();
        }

        public async Task SalvarAsync(Log log)
        {
            _contexto.Logs.Add(log);
            await _contexto.SaveChangesAsync();
        }
    }
}
