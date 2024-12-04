using Dominio;
using Infraestrutura;
using Infraestrutura.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace ConversorLogTests
{
    public class RepositorioLogTests
    {
        private readonly DbContextOptions<ConversorLogContexto> _options;
        private readonly ConversorLogContexto _contexto;
        private readonly RepositorioLog _repositorioLog;

        public RepositorioLogTests()
        {
            _options = new DbContextOptionsBuilder<ConversorLogContexto>()
                .UseInMemoryDatabase(databaseName: "LogDB_Test")
                .Options;

            _contexto = new ConversorLogContexto(_options);
            _repositorioLog = new RepositorioLog(_contexto);
                
        }

        [Fact]
        public async Task SalvarAsync_DeveSalvarLog()
        {
            var log = new Log
            {
                LogEntrada = "entrada",
                LogTransformado = "transformado"
            };

            await _repositorioLog.SalvarAsync(log);
            var resultado = await _contexto.Logs.FirstOrDefaultAsync();

            Assert.NotNull(resultado);
            Assert.Equal("transformado", log.LogTransformado);
        }

    }
}
