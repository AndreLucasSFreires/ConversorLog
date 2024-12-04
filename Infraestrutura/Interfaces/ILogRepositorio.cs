using Dominio;

namespace Infraestrutura.Interfaces
{
    public interface ILogRepositorio
    {
        Task SalvarAsync(Log log);
        Task<Log> ObterPorIdAsync(int id);
        Task<List<Log>> ObterTodos();
    }
}
