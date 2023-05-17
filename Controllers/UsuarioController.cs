using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
public class UsuarioController : ControllerBase
{

	private readonly IUsuarioRepository _usuarioRepository;

	private readonly ILogger<UsuarioController> _logger;
	public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository usuarioRepository)
	{
		_logger = logger;
		_usuarioRepository = usuarioRepository;
	}

	[HttpGet]
	public async Task<IActionResult> Get()
	{
		try
		{
			_logger.LogInformation("Listando todos los usuários");
			var usuarios = await _usuarioRepository.GetAll();
			return Ok(usuarios);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message);
			return StatusCode(500, ex.Message);
		}
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> Get(int id)
	{
		try
		{

			_logger.LogInformation($"Listando usuario con id {id}");
			var usuario = await _usuarioRepository.GetById(x => x.Id == id);
			if (usuario == null)
			{
				_logger.LogError($"Usuario con id {id} no encontrado");
				return NotFound();
			}
			return Ok(usuario);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message);
			return StatusCode(500, ex.Message);
		}
	}

	[HttpPost]
	public async Task<IActionResult> Post([FromBody] Usuario usuario)
	{
		try
		{
			if (ModelState.IsValid)
			{
				_logger.LogInformation($"Insertando usuario {usuario.NombreUsuario}");
				await _usuarioRepository.Insert(usuario);
				return Ok();
			}
			_logger.LogError("Error al insertar usuario");
			return BadRequest();
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message);
			return StatusCode(500, ex.Message);
		}
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> Put(int id, [FromBody] Usuario usuario)
	{
		try
		{
			if (ModelState.IsValid)
			{
				_logger.LogInformation($"Actualizando usuario con id {id}");
				await _usuarioRepository.Update(usuario);
				return Ok();
			}
			_logger.LogError("Error al actualizar usuario");
			return BadRequest();
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message);
			return StatusCode(500, ex.Message);
		}
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(int id)
	{
		try
		{
			var usuario = await _usuarioRepository.GetById(x => x.Id == id);
			if (usuario == null)
			{
				_logger.LogError($"No existe usuario con id {id}, no se pudo realizar la eliminación");
				return NotFound();
			}
			_logger.LogInformation($"Eliminando usuario con id {id}");
			await _usuarioRepository.Delete(usuario);
			return Ok();
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message);
			return StatusCode(500, ex.Message);
		}
	}

	[HttpGet("nombreUsuarioExistente/{nombreUsuario}")]
	public async Task<IActionResult> nombreUsuarioExistente(string nombreUsuario)
	{
		try
		{
			_logger.LogInformation($"Verificando si existe el nombre de usuario {nombreUsuario}");
			return Ok(await _usuarioRepository.nombreUsuarioExistente(nombreUsuario));
		}
		catch (Exception ex)
		{
			_logger.LogError($"Error al verificar si existe el nombre de usuario {nombreUsuario}");
			return StatusCode(500, ex.Message);
		}
	}

	[HttpGet("emailExistente/{email}")]
	public async Task<IActionResult> emailExistente(string email)
	{
		try
		{
			_logger.LogInformation($"Verificando si existe el email {email}");
			return Ok(await _usuarioRepository.emailExistente(email));
		}
		catch (Exception ex)
		{
			_logger.LogError($"Error al verificar si existe el email {email}");
			return StatusCode(500, ex.Message);
		}
	}


}