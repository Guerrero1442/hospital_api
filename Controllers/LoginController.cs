using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.dto;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
namespace backend.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class LoginController : ControllerBase
{
	private readonly IUsuarioRepository _usuarioRepository;

	private readonly ILogger<LoginController> _logger;

	private readonly IConfiguration _config;

	private readonly HospitalContext _context;


	public LoginController(ILogger<LoginController> logger, IUsuarioRepository usuarioRepository, IConfiguration config, HospitalContext context)
	{
		_logger = logger;
		_usuarioRepository = usuarioRepository;
		_config = config;
		_context = context;
	}

	[HttpPost]
	public async Task<IActionResult> Login([FromBody] LoginDTO login)
	{
		try
		{
			var usuario = await _usuarioRepository.Login(login);
			if (usuario is null)
			{
				return NotFound("Usuário o contraseña no son validos");
			}

			// Recupera la información adicional en base al rol del usuario
			object infoAdicional = null;
			switch (usuario.Rol)
			{
				case Rol.Paciente:
					infoAdicional = await _context.Pacientes.FirstOrDefaultAsync(p => p.UsuarioId == usuario.Id);
					break;
				case Rol.Medico:
					infoAdicional = await _context.Medicos.FirstOrDefaultAsync(m => m.UsuarioId == usuario.Id);
					break;
				case Rol.Administrador:
					infoAdicional = await _context.Administradores.FirstOrDefaultAsync(m => m.UsuarioId == usuario.Id);
					break;
			}
			string JWT = GenerateToken(usuario);
			return Ok(new { token = JWT, infoAdicional });
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error en Login");
			return StatusCode(500, "Error en Login");
		}
	}

	private string GenerateToken(Usuario usuario)
	{
		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.Dns, usuario.Email),
			new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
			new Claim(ClaimTypes.Role, usuario.Rol.ToString())
		};

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JWT:Key").Value));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
		var tokenDescriptor = new JwtSecurityToken(
			claims: claims,
			expires: DateTime.Now.AddDays(1),
			signingCredentials: creds
		);
		string token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

		return token;
	}
}