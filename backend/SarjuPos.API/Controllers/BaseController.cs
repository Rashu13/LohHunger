using Microsoft.AspNetCore.Mvc;
using SarjuPos.API.Data;

namespace SarjuPos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController<T> : ControllerBase where T : class
    {
        protected readonly IRepository<T> _repository;

        public BaseController(IRepository<T> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<T>>> GetAll()
        {
            var items = await _repository.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<T>> Get(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public virtual async Task<ActionResult<T>> Create(T item)
        {
            await _repository.AddAsync(item);
            await _repository.SaveChangesAsync();
            return CreatedAtAction("Get", new { id = (item as dynamic).Id }, item);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(int id, T item)
        {
            if (id != (item as dynamic).Id) return BadRequest();
            _repository.Update(item);
            await _repository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null) return NotFound();
            _repository.Remove(item);
            await _repository.SaveChangesAsync();
            return NoContent();
        }
    }
}
