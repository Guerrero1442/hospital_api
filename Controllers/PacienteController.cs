using Microsoft.AspNetCore.Mvc;
using backend.Models;

namespace backend.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PacienteController : GenericController<Paciente>
{
	private readonly IGenericRepository<Paciente> _pacienteRepository;

	private readonly ILogger<GenericController<Paciente>> _logger;
	public PacienteController(IGenericRepository<Paciente> pacienteRepository, ILogger<GenericController<Paciente>> logger) : base(pacienteRepository, logger)
	{
		_pacienteRepository = pacienteRepository;
		_logger = logger;
	}
}
