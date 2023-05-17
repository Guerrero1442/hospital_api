using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.dto;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
namespace backend.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class LoginController : ControllerBase
{
	private readonly IUsuarioRepository _usuarioRepository;

	private readonly ILogger<LoginController> _logger;

	private readonly IConfiguration _config;

	public LoginController(ILogger<LoginController> logger, IUsuarioRepository usuarioRepository, IConfiguration config)
	{
		_logger = logger;
		_usuarioRepository = usuarioRepository;
		_config = config;
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

			string JWT = GenerateToken(usuario);
			return Ok(new { token = JWT });
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
			new Claim(ClaimTypes.Name, usuario.NombreUsuario),
			new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString())
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