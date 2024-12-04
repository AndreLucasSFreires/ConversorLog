namespace Dominio
{
    public class Log
    {
        public int Id { get; set; }
        public string LogEntrada { get; set; } = string.Empty;
        public string LogTransformado { get; set; } = string.Empty ;
        public DateTime DataCriacao { get; set; } = DateTime.Now;
    }
}
