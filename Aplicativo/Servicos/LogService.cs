using Aplicativo.Formatters;
using Aplicativo.Interfaces;
using Dominio;
using Infraestrutura.Interfaces;

namespace Aplicativo.Servicos
{
    public class LogService : ILogService
    {
        private readonly ILogRepositorio _logRepositorio;
        public LogService(ILogRepositorio logRepositorio)
        {
            _logRepositorio = logRepositorio;
        }

        public async Task<Log> ObterLogPorIdAsync(int id)
        => await _logRepositorio.ObterPorIdAsync(id);

        public async Task<List<Log>> ObterLogs()
        => await _logRepositorio.ObterTodos();


        public async Task SalvarLogAsync(string logEntrada, string logTransformado)
        {
            var log = new Log
            {
                LogEntrada = logEntrada,
                LogTransformado = logTransformado,
                DataCriacao = DateTime.UtcNow
            };
            await _logRepositorio.SalvarAsync(log);
        }

        public string TransformarLogAsync(string log)
        {
            ILogFormatter formatter;

            if (log.Contains('#'))
                formatter = new LogMinhaCdnFormatter();
            else
                formatter = new LogAgoraFormatter();

            return formatter.Formatar(log);
        } 
    }
}
