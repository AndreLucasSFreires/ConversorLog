using Dominio;

namespace Aplicativo.Interfaces
{
    public interface ILogService
    {
        string TransformarLogAsync(string log);
        Task SalvarLogAsync(string logEntrada, string logTransformado);
        Task<Log> ObterLogPorIdAsync(int id);
        Task<List<Log>> ObterLogs();
    }
}
