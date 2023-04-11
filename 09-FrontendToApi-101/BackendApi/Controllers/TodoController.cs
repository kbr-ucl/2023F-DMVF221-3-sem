using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using BackendApi.Data;
using BackendApi.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
    private readonly TodoDb _context;
    private readonly IMapper _mapper;

    public TodoController(TodoDb context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/Todoes
    [HttpGet]
    public async Task<ActionResult<List<TodoDto>>> GetTodos()
    {
        if (_context.Todos == null) return NotFound();
        var data = await _context.Todos.ToListAsync();
        return _mapper.Map<List<Todo>, List<TodoDto>>(data);
    }

    // GET: api/Todoes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoDto>> GetTodo(int id)
    {
        if (_context.Todos == null) return NotFound();
        var todo = await _context.Todos.FindAsync(id);
        if (todo == null) return NotFound();

        return _mapper.Map<TodoDto>(todo);
    }

    // PUT: api/Todoes/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut]
    public async Task<IActionResult> PutTodo(TodoDto todo)
    {
        var dbTodo = _mapper.Map<Todo>(todo);
        _context.Update(dbTodo);
        //_context.Todos.Persist(_mapper).InsertOrUpdate(todo);

        //try
        //{
            await _context.SaveChangesAsync();
        //}
        //catch(Exception e) 
        //{
        //    return BadRequest(e.Message);
        //}
        return NoContent();
    }

    // POST: api/Todoes
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Todo>> PostTodo(TodoDto todo)
    {
        if (_context.Todos == null) return Problem("Entity set 'TodoDb.Todos'  is null.");
        _context.Todos.Persist(_mapper).InsertOrUpdate(todo);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetTodo", new {id = todo.Id}, todo);
    }

    // DELETE: api/Todoes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo(int id)
    {
        if (_context.Todos == null) return NotFound();
        //_context.Todos.Persist(_mapper).Remove<TodoDto>(todo);
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