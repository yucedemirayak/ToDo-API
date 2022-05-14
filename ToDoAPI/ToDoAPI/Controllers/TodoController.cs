using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.Data;
using ToDoAPI.Entities;

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _todoContext;
        public TodoController(TodoContext todoContext)
        {
            _todoContext = todoContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int ?id)
        {
            if (id != null)
            {
                return Ok(await _todoContext.Todos.FindAsync(id));
            }
            else
            {
                return Ok(await _todoContext.Todos.ToListAsync());
            }
        }


        [HttpPost]
        public async Task<Todo> Post(Todo todo)
        {
            var addedTodo = await _todoContext.Todos.AddAsync(todo);
            _todoContext.SaveChanges();
            return addedTodo.Entity;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedTodo = new Todo();
            try
            {

                deletedTodo = await _todoContext.Todos.FindAsync(id);
                _todoContext.Todos.Remove(deletedTodo);
                _todoContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return Ok(deletedTodo);
        }

        [HttpPatch]
        public async Task<Todo> Patch(int id)
        {
            var findedTodo = await _todoContext.Todos.FindAsync(id);
            Todo updatedTodo = findedTodo;
            updatedTodo.IsComplete = !findedTodo.IsComplete;
            _todoContext.Todos.Update(updatedTodo);
            _todoContext.SaveChanges();
            return updatedTodo;
        }

        [HttpPut]
        public async Task<Todo> Update(int id, Todo todo)
        {
            var updatedTodo = await _todoContext.Todos.FindAsync(id);
            updatedTodo = todo;
            _todoContext.Todos.Update(updatedTodo);
            _todoContext.SaveChanges();
            return updatedTodo;
        }
    }
}
