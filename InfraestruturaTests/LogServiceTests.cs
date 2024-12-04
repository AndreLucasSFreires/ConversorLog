using Aplicativo.Servicos;
using Dominio;
using Infraestrutura.Interfaces;
using Moq;

namespace InfraestruturaTests
{
    public class LogServiceTests
    {
        Mock<ILogRepositorio> mockRepositorio = new Mock<ILogRepositorio>();
        private LogService logService;
        private string logFormatoMinhaCdn = "312|200|HIT|\"GET /robots.txt HTTP/1.1\"|100.2\n" +
            "101|200|MISS|\"POST /myImages HTTP/1.1\"|319.4\n" +
            "199|404|MISS|\"GET /not-found HTTP/1.1\"|142.9\n" +
            "312|200|INVALIDATE|\"GET /robots.txt HTTP/1.1\"|245.1";

        private string logFormatoAgora = "#Version: 1.0 #Date: 04/12/2024 14:13:22\n" +
            "#Fields: provider http-method status-code uri-path time-taken response-size cache-status\n" +
            "\"MINHA CDN\" GET 200 /robots.txt 100 312 HIT\n" +
            "\"MINHA CDN\" POST 200 /myImages 319 101 MISS\n" +
            "\"MINHA CDN\" GET 404 /not-found 143 199 MISS\n" +
            "\"MINHA CDN\" GET 200 /robots.txt 245 312 REFRESH_HIT";

        public LogServiceTests()
        {
            logService = new LogService(mockRepositorio.Object);
        }

        [Fact]
        public void TransformarLogAsync_DeveTransformarLogFormatoAgoraEmMinhaCdn()
        {
            var logMinhaCdn = logService.TransformarLogAsync(logFormatoAgora);
            Assert.Contains("312|200|HIT|\"GET /robots.txt HTTP/1.1\"", logMinhaCdn);
            Assert.Contains("312|200|INVALIDATE|\"GET /robots.txt HTTP/1.1\"|245", logMinhaCdn);
        }

        [Fact]
        public void TransformarLogAsync_DeveTransformarLogMinhaCdnEmFormatoAgora()
        {
            var logAgora = logService.TransformarLogAsync(logFormatoMinhaCdn);
            Assert.Contains("#Version: 1.0", logAgora);
            Assert.Contains("\"MINHA CDN\" GET 200 /robots.txt 100 312 HIT", logAgora);
            Assert.Contains("\"MINHA CDN\" POST 200 /myImages 319 101 MISS", logAgora);
        }


        [Fact]
        public async Task SalvarLogAsync_DeveSalvarLogNoRepositorio()
        {
            await logService.SalvarLogAsync(logFormatoMinhaCdn,logFormatoAgora);
            mockRepositorio.Verify(repo => repo.SalvarAsync(It.Is<Log>(log =>
            log.LogEntrada == logFormatoMinhaCdn && log.LogTransformado == logFormatoAgora
            && log.DataCriacao <= DateTime.UtcNow
            )),Times.Once);
        }

        [Fact]
        public async Task ObterLogPorIdAsync_DeveRetornarLogTipoAgora()
        {
            var logEsperado = new Log
            {
                Id = 1,
                LogEntrada = "entrada",
                LogTransformado = "transformado"
            };
            mockRepositorio.Setup(repo => repo.ObterPorIdAsync(1)).ReturnsAsync(logEsperado);
            var log = await logService.ObterLogPorIdAsync(1);
            Assert.Equal(logEsperado,log);
        }
    }
}