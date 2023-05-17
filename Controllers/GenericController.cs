using Microsoft.AspNetCore.Mvc;


public class GenericController<T> : ControllerBase where T : class, ModelBase
{

	private readonly IGenericRepository<T> _genericRepository;

	private readonly ILogger<GenericController<T>> _logger;
	public GenericController(IGenericRepository<T> genericRepository, ILogger<GenericController<T>> logger)
	{
		_genericRepository = genericRepository;
		_logger = logger;
	}


	[HttpGet]
	public virtual async Task<IActionResult> Get()
	{
		try
		{
			_logger.LogInformation($"Listando todos los {typeof(T).Name}");
			var result = await _genericRepository.GetAll();
			return Ok(result);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message);
			return StatusCode(500, ex.Message);
		}
	}

	[HttpGet("{id}")]
	public virtual async Task<IActionResult> Get(int id)
	{
		try
		{
			_logger.LogInformation($"Listando {typeof(T).Name} con id {id}");
			var result = await _genericRepository.GetById(x => x.Id == id);
			if (result == null)
			{
				_logger.LogError($"{typeof(T).Name} con id {id} no encontrado");
				return NotFound();
			}
			return Ok(result);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message);
			return StatusCode(500, ex.Message);
		}
	}

	[HttpPost]
	public virtual async Task<IActionResult> Post([FromBody] T entity)
	{
		try
		{
			_logger.LogInformation($"Creando {typeof(T).Name}");
			if (entity == null)
			{
				_logger.LogError($"{typeof(T).Name} es nulo");
				return BadRequest();
			}
			await _genericRepository.Insert(entity);
			return Ok(entity);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message);
			return StatusCode(500, ex.Message);
		}
	}

	[HttpPut("{id}")]
	public virtual async Task<IActionResult> Put(int id, [FromBody] T entity)
	{
		try
		{
			_logger.LogInformation($"Actualizando {typeof(T).Name} con id {id}");
			if (entity == null || !ModelState.IsValid)
			{
				_logger.LogError($"{typeof(T).Name} es nulo o inválido");
				return BadRequest();
			}
			var result = await _genericRepository.GetById(x => x.Id == id);
			if (result == null)
			{
				_logger.LogError($"{typeof(T).Name} con id {id} no encontrado");
				return NotFound();
			}
			await _genericRepository.Update(entity);
			return Ok(entity);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message);
			return StatusCode(500, ex.Message);
		}
	}

	[HttpDelete("{id}")]
	public virtual async Task<IActionResult> Delete(int id)
	{
		try
		{
			_logger.LogInformation($"Eliminando {typeof(T).Name} con id {id}");
			var result = await _genericRepository.GetById(x => x.Id == id);
			if (result == null)
			{
				_logger.LogError($"{typeof(T).Name} con id {id} no encontrado");
				return NotFound();
			}
			await _genericRepository.Delete(result);
			return Ok();
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message);
			return StatusCode(500, ex.Message);

		}
	}
}