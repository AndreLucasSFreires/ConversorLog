using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicativo.DTOs
{
    public class LogDadosDto
    {
        private string _statusCache = string.Empty;
        public string CodigoInterno { get; set; } = string.Empty;
        public string CodigoHTTP { get; set; } = string.Empty;

        public string StatusCache
        {
            get
            {
                string retorno = _statusCache switch
                {
                    "INVALIDATE" => "REFRESH_HIT",
                    "REFRESH_HIT" => "INVALIDATE",
                    _ => _statusCache
                };
                return retorno;
            }
            set => _statusCache = value;
        }
        public string RequisicaoArquivo { get; set; } = string.Empty;
        public string ValorDecimal { get; set; } = string.Empty;
        public string MetodoHttp { get; set; } = string.Empty;
    }
}
