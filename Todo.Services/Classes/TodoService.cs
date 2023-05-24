using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Todo.Services.Interfaces;
using Todo.Services.Models;

namespace Todo.Services.Classes
{
    public class TodoService : ITodoService
    {
        private IWebHostEnvironment _webHostEnvironment;
        private string _path { get { return Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot", "data", "todos.json"); } }

        public TodoService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public TodoDetails AddTodo(TodoDetails todo)
        {
            var todos = GetAllTodos();
            todos = todos.Append(todo);
            string newJsonString = JsonConvert.SerializeObject(todos, Formatting.Indented);
            File.WriteAllText(_path, newJsonString);
            return todo;
        }

        public IEnumerable<TodoDetails> GetAllTodos()
        {
            string jsonString = File.ReadAllText(_path);
            var todos = JsonConvert.DeserializeObject<IEnumerable<TodoDetails>>(jsonString);
            return todos;
        }

        public TodoDetails GetTodo(int id)
        {
            return GetAllTodos().Single(e => e.Id == id);
        }

        public TodoDetails DeleteTodo(int id)
        {
            var deletedTodo = GetTodo(id);
            var todos = GetAllTodos().Where(e => e.Id != id);
            string newJsonString = JsonConvert.SerializeObject(todos, Formatting.Indented);
            File.WriteAllText(_path, newJsonString);
            return deletedTodo;
        }

        public TodoDetails UpdateTodo(TodoDetails todo)
        {
            var todos = GetAllTodos().Where(e => e.Id != todo.Id).Append(todo);
            string newJsonString = JsonConvert.SerializeObject(todos, Formatting.Indented);
            File.WriteAllText(_path, newJsonString);
            return todo;
        }
    }
}
