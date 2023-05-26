
using Microsoft.EntityFrameworkCore;

public interface ICitaRepository : IGenericRepository<Cita>
{
	Task<List<Cita>> GetByPacienteId(int pacienteId);
}

public class CitaRepository : GenericRepository<Cita>, ICitaRepository
{
	private readonly HospitalContext _context;
	public CitaRepository(HospitalContext context) : base(context)
	{
		_context = context;
	}

	public async Task<List<Cita>> GetByPacienteId(int pacienteId)
	{
		return await _context.Citas.Include(c => c.Medico).Where(c => c.PacienteId == pacienteId).ToListAsync();
	}
}