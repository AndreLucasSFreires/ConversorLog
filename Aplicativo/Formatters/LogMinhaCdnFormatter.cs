using Aplicativo.DTOs;
using Aplicativo.Interfaces;
using System.Globalization;
namespace Aplicativo.Formatters
{
    public class LogMinhaCdnFormatter : ILogFormatter
    {
        CultureInfo culture = new CultureInfo("en-US");
        public string Formatar(string logEntrada)
        {
            var linhas = logEntrada.Split("\n", StringSplitOptions.RemoveEmptyEntries);
            var linhasLog = linhas.SkipWhile(linha => linha.StartsWith("#")).ToList();

            List<string> logsMinhaCdn = new();
            foreach (var linha in linhasLog)
            {
                var conteudoLinha = linha.Replace("\"MINHA CDN\"", "").Split(" ", StringSplitOptions.RemoveEmptyEntries);
                LogDadosDto log = new()
                {
                    CodigoInterno = conteudoLinha[4],
                    CodigoHTTP = conteudoLinha[1],
                    StatusCache = conteudoLinha[5],
                    ValorDecimal = conteudoLinha[3],
                    RequisicaoArquivo = conteudoLinha[2],
                    MetodoHttp = conteudoLinha[0],
                };

                string valorDecimal = double.Parse(log.ValorDecimal).ToString("F1", culture);
                string requisicaoArquivo = $"\"{log.MetodoHttp} {log.RequisicaoArquivo} HTTP/1.1\"";

                logsMinhaCdn.Add($"{log.CodigoInterno}|{log.CodigoHTTP}|{log.StatusCache}|{requisicaoArquivo}|" +
                    $"{valorDecimal}");
            }
            return string.Join("\n", logsMinhaCdn);
        }
    }
}
