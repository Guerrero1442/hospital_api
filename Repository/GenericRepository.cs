using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public interface IGenericRepository<T> where T : class
{
	Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null);

	Task<T> GetById(Expression<Func<T, bool>>? filter = null, bool tracking = false);

	Task Update(T entity);

	Task Insert(T entity);

	Task Delete(T entity);
}

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
	private readonly HospitalContext _context;
	private readonly DbSet<T> _dbSet;


	public GenericRepository(HospitalContext context)
	{
		_context = context;
		_dbSet = context.Set<T>();
	}

	public async Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null)
	{
		IQueryable<T> query = _dbSet;
		if (filter != null)
		{
			query = query.Where(filter);
		}
		return await query.ToListAsync();
	}

	public async Task<T> GetById(Expression<Func<T, bool>>? filter = null, bool tracking = false)
	{
		IQueryable<T> query = _dbSet;
		if (!tracking)
		{
			query = query.AsNoTracking();
		}
		if (filter != null)
		{
			query = query.Where(filter);
		}
		return await query.FirstOrDefaultAsync();
	}

	public async Task Update(T entity)
	{
		_context.Entry(entity).State = EntityState.Modified;
		await _context.SaveChangesAsync();
	}


	public async Task Insert(T entity)
	{
		await _dbSet.AddAsync(entity);
		await _context.SaveChangesAsync();
	}
	public async Task Delete(T entity)
	{
		_dbSet.Remove(entity);
		await _context.SaveChangesAsync();
	}
}