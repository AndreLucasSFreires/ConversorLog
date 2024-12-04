using Aplicativo.DTOs;
using Aplicativo.Interfaces;
using Dominio;
using Infraestrutura.Interfaces;
using System.Globalization;

namespace Aplicativo.Servicos
{
    public class LogService : ILogService
    {
        private readonly ILogRepositorio _logRepositorio;
        private readonly List<string> metodosHttp = new() { "GET", "POST" };
        CultureInfo culture = new CultureInfo("en-US");
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
            var transformLog = $"#Version: 1.0\n#Date: {DateTime.UtcNow}\n#Fields: provider http-method " +
                $"status-code uri-path time-taken response-size cache-status\n";

            var linhas = log.Split("\n", StringSplitOptions.RemoveEmptyEntries);
            var logsMinhaCdn = MapearLogsMinhaCdn(linhas);

            foreach (var logMinhaCDN in logsMinhaCdn)
            {
                transformLog += $"\"MINHA CDN\" {IdentificarMetodoHttp(logMinhaCDN.RequisicaoArquivo)} " +
               $"{logMinhaCDN.CodigoHTTP} {IdentificarArquivoConteudoRequisicao(logMinhaCDN.RequisicaoArquivo)} " +
               $"{Math.Round(double.Parse(logMinhaCDN.ValorDecimal, culture),0)} {logMinhaCDN.CodigoInterno} " +
               $"{logMinhaCDN.StatusCache}\r\n";
            }

            return transformLog;
        }

        private static List<LogMinhaCDNDto> MapearLogsMinhaCdn(string[] linhas)
        {
            List<LogMinhaCDNDto> logsMinhaCdn = new();
            foreach (var linha in linhas)
            {
                var conteudoLinha = linha.Split("|");
                logsMinhaCdn.Add(new()
                {
                    CodigoInterno = conteudoLinha[0],
                    CodigoHTTP = conteudoLinha[1],
                    StatusCache = conteudoLinha[2],
                    RequisicaoArquivo = conteudoLinha[3],
                    ValorDecimal = conteudoLinha[4]
                });
            }

            return logsMinhaCdn;
        }

        private string IdentificarArquivoConteudoRequisicao(string linhaConteudoRequisicao)
        {
            string result = linhaConteudoRequisicao;
            metodosHttp.ForEach(metodo =>
            {
                result = result.Replace(metodo, string.Empty);
            });
            return result.Replace("HTTP/1.1", string.Empty).Replace("\"", string.Empty).Trim();
        }

        private string IdentificarMetodoHttp(string linhaConteudoRequisicao)
        {
            string retorno = string.Empty;
            metodosHttp.ForEach(metodo =>
            {
                if (linhaConteudoRequisicao.Contains(metodo))
                    retorno = metodo;
            });
            return retorno;
        }
    }
}
