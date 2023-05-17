using Microsoft.AspNetCore.Mvc;
using backend.Models;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class MedicoController : GenericController<Medico>
{
	private readonly IGenericRepository<Medico> _medicoRepository;

	private readonly ILogger<GenericController<Medico>> _logger;
	public MedicoController(IGenericRepository<Medico> medicoRepository, ILogger<GenericController<Medico>> logger) : base(medicoRepository, logger)
	{
		_medicoRepository = medicoRepository;
		_logger = logger;
	}
}
