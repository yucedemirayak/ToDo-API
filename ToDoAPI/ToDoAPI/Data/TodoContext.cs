using ToDoAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace ToDoAPI.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }

        public DbSet<Todo> Todos { get; set; }
    }
}
