using Aplicativo.DTOs;
using Aplicativo.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConversorLog.Controllers
{
    [ApiController]
    [Route("api/logs")]
    public class LogController : ControllerBase
    {
        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpGet("ObterTodos")]
        public async Task<IActionResult> ObterTodosOsLogs()
        {
            var logs = await _logService.ObterLogs();
            return Ok(logs);
        }

        [HttpGet("ObterLogPorID")]
        public async Task<IActionResult> ObterLogPorID(int id)
        {
            var log = await _logService.ObterLogPorIdAsync(id);
            if (log == null) return NotFound();

            return Ok(log);
        }

        [HttpPost("Salvar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SaveLog([FromBody] LogRequestDto logRequest)
        {
            var logTransformado = _logService.TransformarLogAsync(logRequest.LogEntrada);
            await _logService.SalvarLogAsync(logRequest.LogEntrada, logTransformado);
            return Ok(new {Mensagem = "Log Salvo com sucesso!", formatoAgora = logTransformado });
        }

        [HttpPost("Transformar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult TransformarLog([FromBody] LogRequestDto logRequest)
        {
            var logTransformado = _logService.TransformarLogAsync(logRequest.LogEntrada);
            return Ok(logTransformado);
        }
    }
}
