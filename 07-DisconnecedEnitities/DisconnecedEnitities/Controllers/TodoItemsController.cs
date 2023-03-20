using DisconnecedEnitities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DisconnecedEnitities.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoItemsController : ControllerBase
{
    private readonly TodoDb _context;

    public TodoItemsController(TodoDb context)
    {
        _context = context;
    }

    // GET: api/TodoItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Todo>>> GetTodos()
    {
        return await _context.Todos.ToListAsync();
    }

    // GET: api/TodoItems/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Todo>> GetTodo(int id)
    {
        var todo = await _context.Todos.FindAsync(id);

        if (todo == null) return NotFound();

        return todo;
    }

    // PUT: api/TodoItems/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTodo(int id, Todo todo)
    {
        if (id != todo.Id) return BadRequest();
        // https://learn.microsoft.com/en-us/ef/core/saving/disconnected-entities
        _context.Update(todo);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TodoExists(id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    // POST: api/TodoItems
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Todo>> PostTodo(Todo todo)
    {
        if (_context.Todos == null) return Problem("Entity set 'TodoDb.Todos'  is null.");
        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetTodo", new {id = todo.Id}, todo);
    }

    // DELETE: api/TodoItems/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo(int id)
    {
        if (_context.Todos == null) return NotFound();
        var todo = await _context.Todos.FindAsync(id);
        if (todo == null) return NotFound();

        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TodoExists(int id)
    {
        return (_context.Todos?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}