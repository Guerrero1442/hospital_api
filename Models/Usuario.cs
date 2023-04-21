namespace backend.Models;


public class Usuario
{
	public int Id { get; set; }

	public string NombreUsuario { get; set; }

	public string Email { get; set; }

	public string Password { get; set; }

	public Rol Rol { get; set; } = Rol.Paciente;

}


public class Paciente : Usuario
{
	public string DocumentoIdentificacion { get; set; }

	public string NombreCompleto { get; set; }

	public int Telefono { get; set; }

	public List<string> Beneficiarios { get; set; }
}

public class Medico : Usuario
{
	public string DocumentoIdentificacion { get; set; }

	public string NombreCompleto { get; set; }

	public int Telefono { get; set; }

	public Especialidad Especialidad { get; set; }

	public List<DateTime> Disponibilidad { get; set; }
}

public enum Rol
{
	Paciente,
	Medico,
	Administrador
}

public enum Especialidad
{
	Cardiologia,
	Dermatologia,
	Gastroenterologia,
	Ginecologia,
	Neurologia,
	Oftalmologia,
	Oncologia,
	Pediatria,
	Psicologia,
	Reumatologia,
	Urologia
}