using DisconnecedEnitities.Data;
using DisconnecedEnitities.Dtos;
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
    public async Task<ActionResult<IEnumerable<ToDoDto>>> GetTodos()
    {
        var result = new List<ToDoDto>();
        foreach (var todo in await _context.Todos.ToListAsync())
            result.Add(new ToDoDto
                {Id = todo.Id, IsComplete = todo.IsComplete, Name = todo.Name, Version = todo.Version ?? new byte[1]});

        return result;
    }

    // GET: api/TodoItems/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ToDoDto>> GetTodo(int id)
    {
        var todo = await _context.Todos.FindAsync(id);

        if (todo == null) return NotFound();

        return new ToDoDto
            {Id = todo.Id, IsComplete = todo.IsComplete, Name = todo.Name, Version = todo.Version ?? new byte[1]};
    }

    // PUT: api/TodoItems/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTodo(int id, ToDoEditDto todo)
    {
        if (id != todo.Id) return BadRequest();
        // https://learn.microsoft.com/en-us/ef/core/saving/disconnected-entities

        var model = await _context.Todos.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        if (model == null) return BadRequest();

        model.IsComplete = todo.IsComplete;
        model.Name = todo.Name;
        model.Version = todo.Version;

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
    public async Task<ActionResult<ToDoDto>> PostTodo(ToDoCreateDto todo)
    {
        var model = new ToDo {Name = todo.Name, IsComplete = todo.IsComplete};
        _context.Todos.Add(model);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetTodo", new {id = model.Id}, todo);
    }

    // DELETE: api/TodoItems/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo(int id)
    {
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