using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicativo.DTOs
{
    public class LogMinhaCDNDto
    {
        private string _statusCache = string.Empty;
        public string CodigoInterno { get; set; } = string.Empty;
        public string CodigoHTTP { get; set; } = string.Empty;
        public string StatusCache 
        { 
            get => _statusCache.Replace("INVALIDATE","REFRESH_HIT"); 
            set => _statusCache = value; 
        }
        public string RequisicaoArquivo { get; set; } = string.Empty;
        public string ValorDecimal { get; set; } = string.Empty;
    }
}
