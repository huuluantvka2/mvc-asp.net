using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodosMVC.Models;

namespace TodosMVC.Controllers
{
    public class TodosController : Controller
    {
        ApplicationDbContext _context;
        public TodosController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string searchString)
        {
            //cú pháp LINQ lấy tất cả phần tử trong bản Todos
            var todoLists = from m in _context.Todos
                            select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                string str = searchString.ToLower();
                todoLists = todoLists.Where(x => x.Title.ToLower().Contains(str) || x.Description!.ToLower().Contains(str));
            }
            return View(await todoLists.ToListAsync());
        }

        public IActionResult GetAll(string name, int times)
        {
            ViewData["Name"] = "Hello " + name;
            ViewData["Times"] = times;
            return View(); ;
        }

        public IActionResult List()
        {
            return View(); ;
        }

        // GET: Todos/Edit/4
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Todos.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Todos/Edit/4
        // Bind giúp bảo vệ từ tấn công XSRF, chỉ nhận dữ liệu đúng với những gì controller cần để cập nhật
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Status")] Todo todo)
        {
            if (id != todo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(todo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
