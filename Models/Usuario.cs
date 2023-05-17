namespace backend.Models;


public class Usuario : ModelBase
{
	public int Id { get; set; }

	public string NombreUsuario { get; set; }

	public string Email { get; set; }

	public string Password { get; set; }

	public Rol Rol { get; set; }

}

public enum Rol
{
	Paciente,
	Medico,
	Administrador
}

