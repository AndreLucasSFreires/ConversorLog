using Aplicativo.DTOs;
using Aplicativo.Interfaces;
using System.Globalization;

namespace Aplicativo.Formatters
{
    public class LogAgoraFormatter : ILogFormatter
    {
        CultureInfo culture = new CultureInfo("en-US");
        private readonly List<string> metodosHttp = new() { "GET", "POST" };
        public string Formatar(string logEntrada)
        {
            var transformLog = $"#Version: 1.0\n#Date: {DateTime.UtcNow}\n#Fields: provider http-method " +
                            $"status-code uri-path time-taken response-size cache-status\n";

            var linhas = logEntrada.Split("\n", StringSplitOptions.RemoveEmptyEntries);
            var logsMinhaCdn = MapearLogsMinhaCdn(linhas);

            foreach (var logMinhaCDN in logsMinhaCdn)
            {
                transformLog += $"\"MINHA CDN\" {IdentificarMetodoHttp(logMinhaCDN.RequisicaoArquivo)} " +
               $"{logMinhaCDN.CodigoHTTP} {IdentificarArquivoConteudoRequisicao(logMinhaCDN.RequisicaoArquivo)} " +
               $"{Math.Round(double.Parse(logMinhaCDN.ValorDecimal, culture), 0)} {logMinhaCDN.CodigoInterno} " +
               $"{logMinhaCDN.StatusCache}\r\n";
            }
            return transformLog;
        }
        private static List<LogDadosDto> MapearLogsMinhaCdn(string[] linhas)
        {
            List<LogDadosDto> logsMinhaCdn = new();
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
