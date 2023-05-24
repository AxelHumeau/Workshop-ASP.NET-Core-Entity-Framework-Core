using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Services.Models;

namespace Todo.Services.Interfaces
{
    public interface ITodoService
    {
        IEnumerable<TodoDetails> GetAllTodos();
        TodoDetails GetTodo(int id);
        TodoDetails AddTodo(TodoDetails todo);
        TodoDetails DeleteTodo(int id);
        TodoDetails UpdateTodo(TodoDetails todo);
    }
}
