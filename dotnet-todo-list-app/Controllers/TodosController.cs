using dotnet_todo_list_app.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_todo_list_app.Controllers
{
    [Route("api")]
    public class TodosController : Controller
    {
        private readonly TodoContext _context;

        public TodosController(TodoContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodos()
        {
            return await _context.Todos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            return todo;
        }

        [HttpPost("")]
        public async Task<ActionResult<Todo>> PostTodo([FromBody] Todo todo)
        {
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetTodo", new { id = todo.Id }, todo);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Todo>> PutTodo(int id, [FromBody] Todo todo)
        {
            if (id != todo.Id)
            {
                return BadRequest();
            }
            _context.Entry(todo).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id))
                {
                    return NotFound();
                }
                throw;
            }
            return todo;
        }

        private bool TodoExists(int id)
        {
            return _context.Todos.Any((e) => e.Id == id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}