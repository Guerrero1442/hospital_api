using Microsoft.EntityFrameworkCore;

public interface IMedicoRepository : IGenericRepository<Medico>
{
	Task<List<Medico>> GetByEspecialidad(Especialidad especialidad);
}

public class MedicoRepository : GenericRepository<Medico>, IMedicoRepository
{
	private readonly HospitalContext _context;
	public MedicoRepository(HospitalContext context) : base(context)
	{
		_context = context;
	}

	public async Task<List<Medico>> GetByEspecialidad(Especialidad especialidad)
	{
		return await _context.Medicos.Where(m => m.Especialidad == especialidad).ToListAsync();
	}
}