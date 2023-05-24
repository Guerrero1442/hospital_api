using System.ComponentModel;

namespace backend.Models;


public class Usuario : ModelBase
{
	public int Id { get; set; }

	public string Email { get; set; }

	public string Password { get; set; }

	public Rol Rol { get; set; }

}

public enum Rol
{
	[Description("Paciente")]
	Paciente,
	[Description("Medico")]
	Medico,
	[Description("Administrador")]
	Administrador
}

