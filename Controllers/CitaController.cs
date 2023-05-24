using AutoMapper;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
public class CitaController : GenericController<Cita>
{
	private readonly ICitaRepository _citaRepository;

	private readonly IMedicoRepository _medicoRepository;

	private readonly IPacienteRepository _pacienteRepository;

	private readonly ILogger<GenericController<Cita>> _logger;

	private readonly IMapper _mapper;

	public CitaController(ICitaRepository citaRepository, IMedicoRepository medicoRepository, IPacienteRepository pacienteRepository, ILogger<GenericController<Cita>> logger, IMapper mapper) : base(citaRepository, logger)
	{
		_citaRepository = citaRepository;
		_logger = logger;
		_medicoRepository = medicoRepository;
		_pacienteRepository = pacienteRepository;
		_mapper = mapper;
	}

	[HttpPost("FromDto")]
	public async Task<IActionResult> PostFromDto([FromBody] CrearCitaDTO dto)
	{
		// Validar el modelo
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		// Buscar el médico y el paciente en la base de datos usando los ids proporcionados
		var medico = await _medicoRepository.GetById(x => x.Id == dto.MedicoId);
		var paciente = await _pacienteRepository.GetById(x => x.Id == dto.PacienteId);

		// Comprobar si el médico y el paciente existen
		if (medico == null || paciente == null)
		{
			return BadRequest("El médico y/o el paciente no existen");
		}

		// Crear la nueva cita
		var newCita = _mapper.Map<Cita>(dto);

		// Guardar la cita en la base de datos
		try
		{
			await _citaRepository.Insert(newCita);
			return Ok(newCita);
		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}
	}
}