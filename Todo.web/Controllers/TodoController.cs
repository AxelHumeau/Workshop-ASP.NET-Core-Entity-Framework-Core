using Microsoft.AspNetCore.Mvc;
using Todo.Services.Interfaces;
using Todo.Services.Models;
using Todo.web.Models;

namespace Todo.web.Controllers
{
    public class TodoController : Controller
    {
        private ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        public IActionResult ListeTodo()
        {
            var todos = _todoService.GetAllTodos();
            return View(todos.Select(e => FromDetailsToViewModel(e)));
        }

        public IActionResult DetailsTodo(int id)
        {
            return View(FromDetailsToViewModel(_todoService.GetTodo(id)));
        }

        [HttpGet]
        public IActionResult AjouterTodo()
        {
            return View(new TodoViewModel());
        }

        [HttpPost]
        public IActionResult AjouterTodo(TodoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var maxId = _todoService.GetAllTodos().Select(e => e.Id).Max();
                model.Id = maxId + 1;
                model.CreatedDate = DateTime.Now;
                _todoService.AddTodo(FromViewModelToDetails(model));
                return RedirectToAction("AjouterTodo");
            }
            return View(model);
        }

        private TodoViewModel FromDetailsToViewModel(TodoDetails todoDetails)
        {
            return new TodoViewModel()
            {
                Id = todoDetails.Id,
                Name = todoDetails.Name,
                Description = todoDetails.Description,
                EndDate = todoDetails.EndDate,
                CreatedDate = todoDetails.CreatedDate,
            };
        }

        private TodoDetails FromViewModelToDetails(TodoViewModel todoViewModel)
        {
            return new TodoDetails()
            {
                Id = todoViewModel.Id,
                Name = todoViewModel.Name,
                Description = todoViewModel.Description,
                EndDate = todoViewModel.EndDate,
                CreatedDate = todoViewModel.CreatedDate,
            };
        }
    }
}
