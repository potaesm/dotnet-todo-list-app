﻿namespace dotnet_todo_list_app.Models
{
	public class Todo
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
        public bool Completed { get; set; }
	}
}

