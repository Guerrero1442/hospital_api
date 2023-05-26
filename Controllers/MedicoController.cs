using Microsoft.AspNetCore.Mvc;
using backend.Models;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers;

// [Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class MedicoController : GenericController<Medico>
{
	private readonly IMedicoRepository _medicoRepository;

	private readonly ILogger<GenericController<Medico>> _logger;
	public MedicoController(IMedicoRepository medicoRepository, ILogger<GenericController<Medico>> logger) : base(medicoRepository, logger)
	{
		_medicoRepository = medicoRepository;
		_logger = logger;
	}

	[HttpGet("GetByEspecialidad/{especialidad}")]
	public async Task<IActionResult> GetByEspecialidad(Especialidad especialidad)
	{
		try
		{
			var medicos = await _medicoRepository.GetByEspecialidad(especialidad);
			return Ok(medicos);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error en GetByEspecialidad");
			return StatusCode(500, "Error en GetByEspecialidad");
		}
	}
}
