using Microsoft.EntityFrameworkCore;
namespace dotnet_todo_list_app.Models
{
	public class TodoContext : DbContext
    {
		public TodoContext(DbContextOptions<TodoContext> options) : base(options)
		{
		}
		public DbSet<Todo> Todos { get; set; }
	}
}

