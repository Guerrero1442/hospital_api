public interface IPacienteRepository : IGenericRepository<Paciente>
{

}

public class PacienteRepository : GenericRepository<Paciente>, IPacienteRepository
{
	private readonly HospitalContext _context;
	public PacienteRepository(HospitalContext context) : base(context)
	{
		_context = context;
	}
}