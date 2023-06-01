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

	[HttpGet]
	[Route("profile")]
	public async Task<IActionResult> GetProfile()
	{
		try
		{
			// Intenta obtener el token del encabezado de autorización.
			var authorizationHeader = this.Request.Headers["Authorization"].ToString();
			if (string.IsNullOrEmpty(authorizationHeader))
			{
				return BadRequest("No authorization header provided.");
			}

			// Quita el prefijo "Bearer " del token.
			var token = authorizationHeader.Replace("Bearer ", "");

			// Configura las credenciales de validación con la misma clave que usaste para firmar el token.
			var key = Encoding.UTF8.GetBytes(_config.GetSection("JWT:Key").Value);
			var validationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(key),
				ValidateIssuer = false,
				ValidateAudience = false,
			};

			// Lee y valida el token.
			var handler = new JwtSecurityTokenHandler();
			var jwt = handler.ReadJwtToken(token);

			// Valida el token y obtén los claims.
			handler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
			var claims = ((JwtSecurityToken)validatedToken).Claims;

			// Extrae los datos del usuario del token.
			var userIdClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Dns);
			var userRolClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
			if (userIdClaim == null || userRolClaim == null)
			{
				return BadRequest("Invalid token");
			}

			var userEmail = userIdClaim.Value;
			var userRol = userRolClaim.Value;

			switch (userRol)
			{
				case "Paciente":
					var paciente = await _context.Pacientes.FirstOrDefaultAsync(p => p.Usuario.Email == userEmail);
					if (paciente != null)
					{
						return Ok(paciente);
					}
					break;
				case "Medico":
					var medico = await _context.Medicos.FirstOrDefaultAsync(m => m.Usuario.Email == userEmail);
					if (medico != null)
					{
						return Ok(medico);
					}
					break;
				case "Administrador":
					var admin = await _context.Administradores.FirstOrDefaultAsync(a => a.Usuario.Email == userEmail);
					if (admin != null)
					{
						return Ok(admin);
					}
					break;
				default:
					return BadRequest("Invalid user role");
			}

			return NotFound("User not found");
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error retrieving user profile");
			return StatusCode(500, "Error retrieving user profile");
		}
	}

}