using backend.dto;
using backend.Models;

public interface IUsuarioRepository : IGenericRepository<Usuario>
{
	Task<bool> nombreUsuarioExistente(string nombreUsuario);

	Task<bool> emailExistente(string email);

	Task<Usuario> Login(LoginDTO login);
}

public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
{
	private readonly HospitalContext _context;
	public UsuarioRepository(HospitalContext context) : base(context)
	{
		_context = context;
	}

	public async Task<bool> emailExistente(string email)
	{
		var usuario = await Task.Run(() => _context.Usuarios.FirstOrDefault(x => x.Email == email));
		if (usuario != null)
		{
			return true;
		}
		return false;
	}

	public async Task<bool> nombreUsuarioExistente(string nombreUsuario)
	{
		var usuario = await Task.Run(() => _context.Usuarios.FirstOrDefault(x => x.NombreUsuario == nombreUsuario));
		if (usuario != null)
		{
			return true;
		}
		return false;
	}

	public async Task<Usuario> Login(LoginDTO login)
	{
		var usuario = await Task.Run(() => _context.Usuarios.FirstOrDefault(x => x.NombreUsuario == login.NombreUsuario && x.Password == login.Password));
		if (usuario == null)
		{
			return null;
		}
		return usuario;
	}
}