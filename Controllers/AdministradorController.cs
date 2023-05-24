using Microsoft.AspNetCore.Mvc;
using backend.Models;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers;

// [Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class AdministradorController : GenericController<Administrador>
{
	private readonly IGenericRepository<Administrador> _administradorRepository;

	private readonly ILogger<GenericController<Administrador>> _logger;
	public AdministradorController(IGenericRepository<Administrador> administradorRepository, ILogger<GenericController<Administrador>> logger) : base(administradorRepository, logger)
	{
		_administradorRepository = administradorRepository;
		_logger = logger;
	}
}
