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
            return View(todos.OrderBy(e => e.Id).Select(e => FromDetailsToViewModel(e)));
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
                var todos = _todoService.GetAllTodos().Select(e => e.Id);
                if (todos.Count() == 0)
                    model.Id = 0;
                else
                    model.Id = todos.Max() + 1;
                model.CreatedDate = DateTime.Now;
                _todoService.AddTodo(FromViewModelToDetails(model));
                return RedirectToAction("AjouterTodo");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ModifierTodo(int id)
        {
            return View(FromDetailsToViewModel(_todoService.GetTodo(id)));
        }

        [HttpPost]
        public IActionResult ModifierTodo(TodoViewModel model)
        {
            if (ModelState.IsValid)
            {
                _todoService.UpdateTodo(FromViewModelToDetails(model));
                return RedirectToAction("ModifierTodo", model.Id);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult SupprimerTodo(int id)
        {
            return View(FromDetailsToViewModel(_todoService.GetTodo(id)));
        }

        [HttpPost]
        public IActionResult SupprimerTodo(TodoViewModel model)
        {
            if (ModelState.IsValid)
            {
                _todoService.DeleteTodo(model.Id);
                return RedirectToAction("ListeTodo");
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
